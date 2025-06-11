// TimelineControl.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;               // <-- För Debug.WriteLine
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using AdvancedToDoList.Models;

namespace AdvancedToDoList.Controls
{
    public class TimelineControl : Control
    {
        // 1) Events för att meddela MainForm att användaren klickat/skapat/flyttat ett projekt
        public event EventHandler<RectangleClickedEventArgs> ProjectClicked;
        public event EventHandler<RectangleDraggedEventArgs> ProjectDragged;
        public event EventHandler<RectangleCreatedEventArgs> ProjectCreated;

        // 2) Lista över projekt som ska visas
        private List<Project> _projects = new List<Project>();
        public List<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                Debug.WriteLine($"[TimelineControl] Sätter Projects, antal projekt = {_projects.Count}");
                Invalidate();
            }
        }

        // 3) Tidsvy‐inställningar och layoutparametrar
        private DateTime _viewStart;
        private DateTime _viewEnd;
        private float _pixelsPerDay = 5f;      // Zoom‐nivå: pixlar per dag
        private int _headerHeight = 40;        // Höjd på datumrubrik‐område
        private int _rowHeight = 30;           // Höjd per projekt‐rad

        // 4) State för musinteraktion
        private bool _isPanning = false;       // Högerklick + dra för att panorera
        private Point _lastMousePos;           // För pan/drag

        private bool _isDraggingProject = false;
        private Project _draggingProject = null;
        private Point _dragStartMouse;
        private DateTime _dragStartDate;       // Startdatum när man började dra ett projekt

        private bool _potentialClick = false;
        private Project _clickedProject = null;

        // Fält för snapping vid drag
        private DateTime _dragOrigStartDate;
        private DateTime _dragOrigEndDate;

        // Fält för resizing
        private bool _isResizingLeft = false;
        private bool _isResizingRight = false;

        // 5) Konstruktor
        public TimelineControl()
        {
            // Sätt double buffering för att undvika blinkningar
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            // Standardvy: 30 dagar runt idag
            _viewStart = DateTime.Today.AddDays(-15);
            _viewEnd = DateTime.Today.AddDays(15);

            // Anslut event‐handlers
            MouseWheel += TimelineControl_MouseWheel;
            MouseDown += TimelineControl_MouseDown;
            MouseMove += TimelineControl_MouseMove;
            MouseUp += TimelineControl_MouseUp;

            this.BackColor = Color.White;
            Debug.WriteLine("[TimelineControl] Konstruktor färdig, vyStart = " + _viewStart.ToShortDateString() +
                            ", vyEnd = " + _viewEnd.ToShortDateString());
        }

        #region Mus‐händelser (MouseDown, MouseMove, MouseUp, MouseWheel)

        private void TimelineControl_MouseWheel(object sender, MouseEventArgs e)
        {
            Debug.WriteLine($"[TimelineControl] MouseWheel: Delta={e.Delta}, X={e.X}, Y={e.Y}");
            const float zoomFactor = 1.1f;
            if (e.Delta > 0)
            {
                _pixelsPerDay *= zoomFactor;
                Debug.WriteLine($"[TimelineControl] Zoom in, pixelsPerDay = {_pixelsPerDay}");
            }
            else
            {
                _pixelsPerDay /= zoomFactor;
                if (_pixelsPerDay < 1f) _pixelsPerDay = 1f;
                Debug.WriteLine($"[TimelineControl] Zoom out, pixelsPerDay = {_pixelsPerDay}");
            }
            Invalidate();
        }

        private void TimelineControl_MouseDown(object sender, MouseEventArgs e)
        {
            _lastMousePos = e.Location;
            Debug.WriteLine($"[TimelineControl] MouseDown: Button={e.Button}, X={e.X}, Y={e.Y}");

            if (e.Button == MouseButtons.Right)
            {
                _isPanning = true;
                Cursor = Cursors.Hand;
                Debug.WriteLine("[TimelineControl] Startar pan (högerklick + dra)");
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                var hit = HitTestProject(e.Location);
                if (hit.HasValue)
                {
                    var proj = hit.Value.Project;
                    var rect = GetProjectRectangle(proj);

                    // 1) Kolla om vi klickade nära vänsterkanten → resize vänster
                    if (Math.Abs(e.X - rect.Left) <= 5)
                    {
                        _isResizingLeft = true;
                        _clickedProject = proj;
                        _dragOrigStartDate = proj.StartDate;
                        _dragOrigEndDate = proj.EndDate;
                        _dragStartDate = ScreenToDate(e.X).Date;
                        Debug.WriteLine($"[TimelineControl] Startar resize-left på \"{proj.Title}\".");
                        return;
                    }
                    // 2) Kolla om vi klickade nära högerkanten → resize höger
                    if (Math.Abs(e.X - rect.Right) <= 5)
                    {
                        _isResizingRight = true;
                        _clickedProject = proj;
                        _dragOrigStartDate = proj.StartDate;
                        _dragOrigEndDate = proj.EndDate;
                        _dragStartDate = ScreenToDate(e.X).Date;
                        Debug.WriteLine($"[TimelineControl] Startar resize-right på \"{proj.Title}\".");
                        return;
                    }
                    // 3) Annars vill vi kanske move:a projektet
                    _potentialClick = true;
                    _clickedProject = proj;
                    _dragOrigStartDate = proj.StartDate;
                    _dragOrigEndDate = proj.EndDate;
                    _dragStartMouse = e.Location;
                    _dragStartDate = ScreenToDate(e.X).Date;
                    Debug.WriteLine($"[TimelineControl] Möjligt klick/move på projekt \"{proj.Title}\".");
                    return;
                }
                // Klick på tomt område = skapa nytt (oförändrat)
                Debug.WriteLine("[TimelineControl] Klick på tom yta i MouseDown – vänta på MouseUp för att skapa nytt projekt.");
            }
        }


        private void TimelineControl_MouseMove(object sender, MouseEventArgs e)
        {
            // 1) Om vi resize:  
            if ((_isResizingLeft || _isResizingRight) && _clickedProject != null)
            {
                var targetDate = ScreenToDate(e.X).Date;
                var dayDiff = (targetDate - _dragStartDate).Days;

                if (_isResizingLeft)
                {
                    // Nya startdatum = originalStart + heltalsdagar, men aldrig efter enddate - 1 dag
                    var newStart = _dragOrigStartDate.AddDays(dayDiff);
                    if (newStart < _clickedProject.EndDate)
                        _clickedProject.StartDate = newStart;
                    Debug.WriteLine($"[TimelineControl] Resizing-left “{_clickedProject.Title}” till {_clickedProject.StartDate:yyyy-MM-dd}");
                }
                else if (_isResizingRight)
                {
                    // Nya slutdatum = originalEnd + heltalsdagar, men aldrig före startdate + 1 dag
                    var newEnd = _dragOrigEndDate.AddDays(dayDiff);
                    if (newEnd > _clickedProject.StartDate)
                        _clickedProject.EndDate = newEnd;
                    Debug.WriteLine($"[TimelineControl] Resizing-right “{_clickedProject.Title}” till {_clickedProject.EndDate:yyyy-MM-dd}");
                }

                Invalidate();
                ProjectDragged?.Invoke(this, new RectangleDraggedEventArgs(_clickedProject));
                return;
            }

            // 2) Om vi panorerar
            if (_isPanning)
            {
                // Panorering av tidsvy
                var dx = e.X - _lastMousePos.X;
                var dayDelta = dx / _pixelsPerDay;
                _viewStart = _viewStart.AddDays(-dayDelta);
                _viewEnd = _viewEnd.AddDays(-dayDelta);
                _lastMousePos = e.Location;
                Debug.WriteLine($"[TimelineControl] Pan: Flyttade vy med {dayDelta:F2} dagar. Ny vyStart={_viewStart.ToShortDateString()}, vyEnd={_viewEnd.ToShortDateString()}");
                Invalidate();
            }

            // 3) Om vi ska börja move:a projektet:
            if (_potentialClick && _clickedProject != null)
            {
                var dx = Math.Abs(e.X - _dragStartMouse.X);
                var dy = Math.Abs(e.Y - _dragStartMouse.Y);
                if (dx > 3 || dy > 3)
                {
                    _isDraggingProject = true;
                    _potentialClick = false;
                    Debug.WriteLine($"[TimelineControl] Övergår till drag på projekt \"{_clickedProject.Title}\".");
                }
            }

            // 4) Om vi är i “drag mode” prisar vi för projecktfyllningen i heltalsdagar
            if (_isDraggingProject && _clickedProject != null)
            {
                // Räkna ut hur många hela dagar musen flyttat
                var targetDate = ScreenToDate(e.X).Date;
                var dayDiff = (targetDate - _dragStartDate).Days;

                // Sätt nya datum baserat på original + dagDiff
                _clickedProject.StartDate = _dragOrigStartDate.AddDays(dayDiff);
                _clickedProject.EndDate = _dragOrigEndDate.AddDays(dayDiff);

                Debug.WriteLine($"[TimelineControl] Draggar projekt \"{_clickedProject.Title}\" till {_clickedProject.StartDate:yyyy-MM-dd} – {_clickedProject.EndDate:yyyy-MM-dd}");
                Invalidate();
                ProjectDragged?.Invoke(this, new RectangleDraggedEventArgs(_clickedProject));
                return;
            }

            // 5) Standard-curshantering om vi inte gör något ovan
            if (HitTestProject(e.Location).HasValue)
            {
                Cursor = Cursors.SizeAll;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void TimelineControl_MouseUp(object sender, MouseEventArgs e)
        {
            Debug.WriteLine($"[TimelineControl] MouseUp: Button={e.Button}, X={e.X}, Y={e.Y}, _isDraggingProject={_isDraggingProject}, _isResizingLeft={_isResizingLeft}, _isResizingRight={_isResizingRight}");

            if (e.Button == MouseButtons.Right && _isPanning)
            {
                _isPanning = false;
                Cursor = Cursors.Default;
                Debug.WriteLine("[TimelineControl] Avslutar pan.");
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                // 1) Om vi avslutar resize
                if (_isResizingLeft || _isResizingRight)
                {
                    Debug.WriteLine($"[TimelineControl] Avslutar resize på \"{_clickedProject?.Title}\".");
                    _isResizingLeft = false;
                    _isResizingRight = false;
                    _clickedProject = null;
                    return;
                }

                // 2) Om vi avslutar drag
                if (_isDraggingProject && _clickedProject != null)
                {
                    Debug.WriteLine($"[TimelineControl] Avslutar drag av projekt \"{_clickedProject.Title}\".");
                    _isDraggingProject = false;
                    _clickedProject = null;
                    return;
                }

                // 3) Om det var ett vanligt klick → ProjectClicked (oförändrat)
                if (_potentialClick && _clickedProject != null)
                {
                    var rect = GetProjectRectangle(_clickedProject);
                    Debug.WriteLine($"[TimelineControl] Klick på projekt \"{_clickedProject.Title}\" – sänder ProjectClicked.");
                    ProjectClicked?.Invoke(this, new RectangleClickedEventArgs(_clickedProject, rect));
                    _clickedProject = null;
                    _potentialClick = false;
                    return;
                }

                // 4) Klick på tom yta → skapa nytt projekt (oförändrat)
                var clickedDate = ScreenToDate(e.X);
                var start = clickedDate.Date;
                var end = start.AddDays(7);
                var row = GetRowFromY(e.Y);
                Debug.WriteLine($"[TimelineControl] Klick på tom yta: sänder ProjectCreated(start={start:yyyy-MM-dd}, end={end:yyyy-MM-dd}, row={row})");
                ProjectCreated?.Invoke(this, new RectangleCreatedEventArgs(start, end, row));
                var halfDays = (_viewEnd - _viewStart).TotalDays / 2;
                _viewStart = start.AddDays(-halfDays);
                _viewEnd = start.AddDays(halfDays);
                Invalidate();
                // Återställ potentialClick
                _potentialClick = false;
            }
        }


        #endregion

        #region Rit‐ och HitTest‐metoder

        /// <summary>
        /// Returnerar (Project, Rectangle) om klicket träffar en projektrektangel.
        /// Annars null.
        /// </summary>
        private (Project Project, Rectangle Rect)? HitTestProject(Point pt)
        {
            foreach (var proj in _projects)
            {
                var rect = GetProjectRectangle(proj);
                if (rect.Contains(pt))
                {
                    Debug.WriteLine($"[TimelineControl] HitTestProject: träffade projekt \"{proj.Title}\" vid punkt X={pt.X}, Y={pt.Y}.");
                    return (proj, rect);
                }
            }
            // Ta bort eller kommentera bort nedanstående rad för att slippa “ingen projektrektangel” i loggen
            // Debug.WriteLine($"[TimelineControl] HitTestProject: ingen projektrektangel vid X={pt.X}, Y={pt.Y}.");
            return null;
        }


        /// <summary>
        /// Konverterar datum till pixel‐koordinat X.
        /// </summary>
        private int DateToScreen(DateTime dt)
        {
            var totalDaysFromStart = (dt - _viewStart).TotalDays;
            int x = (int)(totalDaysFromStart * _pixelsPerDay);
            //Debug.WriteLine($"[TimelineControl] DateToScreen: Datum {dt.ToShortDateString()} => X={x}");
            return x;
        }

        /// <summary>
        /// Konverterar pixel X till datum.
        /// </summary>
        private DateTime ScreenToDate(int x)
        {
            var daysOffset = x / _pixelsPerDay;
            DateTime dt = _viewStart.AddDays(daysOffset);
            //Debug.WriteLine($"[TimelineControl] ScreenToDate: X={x} => datum {dt.ToShortDateString()}");
            return dt;
        }

        /// <summary>
        /// Beräkna radindex utifrån Y‐koordinat (om man vill ha flera rader).
        /// </summary>
        private int GetRowFromY(int y)
        {
            if (y < _headerHeight) return 0;
            return (y - _headerHeight) / _rowHeight;
        }

        /// <summary>
        /// Beräkna rektangel på skärmen för ett givet Project‐objekt.
        /// </summary>
        private Rectangle GetProjectRectangle(Project proj)
        {
            int x1 = DateToScreen(proj.StartDate);
            int x2 = DateToScreen(proj.EndDate);
            int width = Math.Max(5, x2 - x1);
            int row = proj.Row;                // Använd den sparade raden istället för 0
            int y = _headerHeight + row * _rowHeight;
            return new Rectangle(x1, y + 5, width, _rowHeight - 10);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawHeader(g);
            DrawTimeGrid(g);
            DrawProjects(g);
        }

        private void DrawTimeGrid(Graphics g)
        {
            var penMinor = Pens.LightGray;
            var penWeek = Pens.Gray;      // Veckostart (måndag)
            var penMonth = Pens.Black;     // Månadsskifte (endast vid tillräcklig utzoomning)

            // 1) Räkna fram stepDays (samma logik som i DrawHeader)
            int minLabelPx = 80;
            int stepDays = Math.Max(1, (int)Math.Ceiling(minLabelPx / _pixelsPerDay));

            // 2) Utgångspunkt: hur långt in på dagen viewStart ligger (i dagar, ex. 0.25 om kl 06:00)
            double fractionalDays = (_viewStart - _viewStart.Date).TotalDays;
            // 3) Därför hamnar midnatt för dt = viewStart.Date på:
            double x = -fractionalDays * _pixelsPerDay;

            // 4) Stega dag för dag tills vi når kontrollens bredd
            DateTime dt = _viewStart.Date;
            while (x <= Width)
            {
                int xi = (int)Math.Round(x);
                if (xi >= 0 && xi <= Width)
                {
                    // a) Om vi är vid månadens första dag OCH utzoomat (stepDays >= 7)
                    if (stepDays >= 7 && dt.Day == 1)
                    {
                        g.DrawLine(penMonth, xi, _headerHeight, xi, Height);
                    }
                    // b) Annars om vi är måndag (veckostart)
                    else if (dt.DayOfWeek == DayOfWeek.Monday)
                    {
                        g.DrawLine(penWeek, xi, _headerHeight, xi, Height);
                    }
                    // c) Annars “minor” daglig linje
                    else
                    {
                        g.DrawLine(penMinor, xi, _headerHeight, xi, Height);
                    }
                }

                // Nästa dag
                dt = dt.AddDays(1);
                x += _pixelsPerDay;
            }
        }

        private void DrawHeader(Graphics g)
        {
            // 1) Rita bakgrund och månadsblock (oförändrat)
            using (var brushBg = new SolidBrush(Color.FromArgb(230, 230, 230)))
                g.FillRectangle(brushBg, 0, 0, this.ClientSize.Width, _headerHeight);

            Color[] monthColors = new Color[]
            {
        Color.LightCoral, Color.LightGreen, Color.LightSkyBlue,
        Color.LightSalmon, Color.LightGoldenrodYellow, Color.Plum,
        Color.PeachPuff, Color.PaleTurquoise, Color.Khaki,
        Color.MediumAquamarine, Color.Tomato, Color.MistyRose
            };

            double visibleDays = this.ClientSize.Width / _pixelsPerDay;
            DateTime viewStartDate = _viewStart.Date;
            DateTime viewEndVisible = _viewStart.AddDays(visibleDays).Date;

            DateTime firstMonth = new DateTime(viewStartDate.Year, viewStartDate.Month, 1);
            DateTime lastMonth = new DateTime(viewEndVisible.Year, viewEndVisible.Month, 1);

            using (var monthFont = new Font(Font.FontFamily, 10f, FontStyle.Bold))
            using (var monthFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                float monthY = _headerHeight * 0.25f;
                for (DateTime month = firstMonth; month <= lastMonth; month = month.AddMonths(1))
                {
                    DateTime monthStart = month;
                    DateTime monthEnd = month.AddMonths(1);

                    double dxStart = (monthStart - _viewStart).TotalDays * _pixelsPerDay;
                    double dxEnd = (monthEnd - _viewStart).TotalDays * _pixelsPerDay;

                    int x1 = (int)Math.Floor(dxStart);
                    int x2 = (int)Math.Ceiling(dxEnd);

                    if (x2 < 0 || x1 > this.ClientSize.Width)
                        continue;

                    int drawX1 = Math.Max(0, x1);
                    int drawX2 = Math.Min(this.ClientSize.Width, x2);
                    int drawWidth = drawX2 - drawX1;

                    int colorIndex = (month.Month - 1) % monthColors.Length;
                    using (var monthBrush = new SolidBrush(monthColors[colorIndex]))
                    {
                        g.FillRectangle(monthBrush, drawX1, 0, drawWidth, _headerHeight);
                    }

                    string monthName = month.ToString("MMMM", new CultureInfo("sv-SE"));
                    g.DrawString(
                        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(monthName),
                        monthFont,
                        Brushes.Black,
                        drawX1 + drawWidth / 2f,
                        monthY,
                        monthFormat
                    );
                }
            }

            // 2) Trösklar för full datum, kort datum och veckovy
            const float fullDateThreshold = 100f; // ≥ visar “1 juni”
            const float shortDateThreshold = 60f;  // ≥ visar “1 jun”
            const float weekTextThreshold = 70f;  // vecka‐etikett kräver minst denna veckobredd

            bool useFullDate = _pixelsPerDay >= fullDateThreshold;
            bool useShortDate = _pixelsPerDay < fullDateThreshold && _pixelsPerDay >= shortDateThreshold;
            bool useWeekView = _pixelsPerDay < shortDateThreshold;

            using (var dayFont = new Font(Font.FontFamily, 9f, FontStyle.Regular))
            {
                if (useWeekView)
                {
                    // VECKOVY: visa veckostaplar och text om det finns plats
                    DateTime firstMon = viewStartDate;
                    while (firstMon.DayOfWeek != DayOfWeek.Monday)
                        firstMon = firstMon.AddDays(1);

                    float weekWidth = _pixelsPerDay * 7f; // Ändrat till float
                    bool drawWeekText = weekWidth >= weekTextThreshold;
                    using (var weekFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    using (var linePen = new Pen(Color.Gray))
                    {
                        double x = (firstMon - _viewStart).TotalDays * _pixelsPerDay;
                        while (x <= this.ClientSize.Width)
                        {
                            int xi = (int)Math.Round(x);
                            if (xi >= 0 && xi <= this.ClientSize.Width)
                            {
                                // Rita vertikal linje vid veckostart
                                g.DrawLine(linePen, xi, 0, xi, _headerHeight);

                                if (drawWeekText)
                                {
                                    int weekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                        firstMon,
                                        CalendarWeekRule.FirstFourDayWeek,
                                        DayOfWeek.Monday);
                                    string lbl = $"Vecka {weekNum}";
                                    // Veckans mittpunkt: xi + (weekWidth / 2)
                                    float centerX = xi + (weekWidth / 2f);
                                    g.DrawString(lbl, dayFont, Brushes.Black, centerX, _headerHeight * 0.5f, weekFormat);
                                }
                            }
                            firstMon = firstMon.AddDays(7);
                            x += weekWidth;
                        }
                    }
                }
                else if (useShortDate)
                {
                    // KORT DATUM: centrera “d MMM” i varje dagskolumn
                    DateTime dt = viewStartDate;
                    double xLabel = -((_viewStart - _viewStart.Date).TotalDays * _pixelsPerDay);
                    using (var centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        while (xLabel <= this.ClientSize.Width)
                        {
                            int xi = (int)Math.Round(xLabel);
                            if (xi >= 0 && xi <= this.ClientSize.Width)
                            {
                                string shortDate = dt.ToString("d MMM", new CultureInfo("sv-SE")).ToLower();
                                float centerX = xi + _pixelsPerDay / 2f;
                                g.DrawString(shortDate, dayFont, Brushes.Black, centerX, _headerHeight * 0.75f, centerFormat);
                            }
                            dt = dt.AddDays(1);
                            xLabel += _pixelsPerDay;
                        }
                    }
                }
                else // useFullDate
                {
                    // FULL DATUM: centrera “d MMMM” i varje dagskolumn
                    DateTime dt = viewStartDate;
                    double xLabel = -((_viewStart - _viewStart.Date).TotalDays * _pixelsPerDay);
                    using (var centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        while (xLabel <= this.ClientSize.Width)
                        {
                            int xi = (int)Math.Round(xLabel);
                            if (xi >= 0 && xi <= this.ClientSize.Width)
                            {
                                string rawDayText = dt.ToString("d MMMM", new CultureInfo("sv-SE"));
                                string dayText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rawDayText);
                                float centerX = xi + _pixelsPerDay / 2f;
                                g.DrawString(dayText, dayFont, Brushes.Black, centerX, _headerHeight * 0.75f, centerFormat);
                            }
                            dt = dt.AddDays(1);
                            xLabel += _pixelsPerDay;
                        }
                    }
                }
            }

            // 3) Underliggande linje längst ner i headern
            g.DrawLine(Pens.Gray, 0, _headerHeight - 1, this.ClientSize.Width, _headerHeight - 1);
        }

        private float GetProjectCompletionFraction(Project proj)
        {
            int totalParts = 0;
            int completedParts = 0;
            foreach (var task in proj.Tasks)
            {
                foreach (var part in task.Parts)    // Antar att TaskItem har en lista 'Parts' av typ List<TaskPart>
                {
                    totalParts++;
                    if (part.IsCompleted) completedParts++;
                }
            }
            if (totalParts == 0) return 0f;
            return (float)completedParts / totalParts;
        }


        private void DrawProjects(Graphics g)
        {
            foreach (var proj in _projects)
            {
                var rect = GetProjectRectangle(proj);
                // 1) Grundfärg orange om inget är klart, grön om allt klart
                float fraction = GetProjectCompletionFraction(proj);
                Color baseColor = (fraction >= 1f) ? Color.Green
                                   : (fraction <= 0f) ? Color.Orange
                                   : Color.Orange;
                using (var brushBg = new SolidBrush(Color.FromArgb(150, baseColor)))
                {
                    g.FillRectangle(brushBg, rect);
                }

                // 2) Om någon procent är klar (0 < fraction < 1), rita en grön “progress‐bar” ovanpå
                if (fraction > 0f && fraction < 1f)
                {
                    int greenWidth = (int)(rect.Width * fraction);
                    var greenRect = new Rectangle(rect.X, rect.Y, greenWidth, rect.Height);
                    using (var brushFg = new SolidBrush(Color.FromArgb(200, Color.Green)))
                    {
                        g.FillRectangle(brushFg, greenRect);
                    }
                }

                // 3) Ramen runt projektet (behåll alltid mörkblå ram)
                g.DrawRectangle(Pens.DarkBlue, rect);

                // 4) Skriv projektets titel ovanpå
                var name = proj.Title;
                var sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter };
                g.DrawString(name, this.Font, Brushes.Black, rect, sf);
            }
        }


        #endregion
    }

    #region EventArgs‐klasser

    public class RectangleClickedEventArgs : EventArgs
    {
        public Project Project { get; }
        public Rectangle Rectangle { get; }

        public RectangleClickedEventArgs(Project proj, Rectangle rect)
        {
            Project = proj;
            Rectangle = rect;
        }
    }

    public class RectangleDraggedEventArgs : EventArgs
    {
        public Project Project { get; }

        public RectangleDraggedEventArgs(Project proj)
        {
            Project = proj;
        }
    }

    public class RectangleCreatedEventArgs : EventArgs
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public int Row { get; }

        public RectangleCreatedEventArgs(DateTime start, DateTime end, int row)
        {
            Start = start;
            End = end;
            Row = row;
        }
    }

    #endregion
}
