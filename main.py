import os
import json
import uuid
from flask import Flask, request, redirect, url_for, render_template_string, abort
from markupsafe import Markup

DATA_FILE = os.path.join(os.path.dirname(__file__), 'projects.json')


def load_data():
    if os.path.exists(DATA_FILE):
        with open(DATA_FILE, 'r', encoding='utf-8') as f:
            try:
                return json.load(f)
            except json.JSONDecodeError:
                return {"AllPersons": [], "AllProjects": []}
    return {"AllPersons": [], "AllProjects": []}


def save_data(data):
    with open(DATA_FILE, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=2)


app = Flask(__name__)


BASE_PAGE = '''<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>{{ title }}</title>
    <style>
        body { font-family: Arial, sans-serif; margin:0; padding:20px; background:#f2f2f2; }
        .container { background:#fff; padding:20px; border-radius:8px; max-width:800px; margin:auto; box-shadow:0 2px 4px rgba(0,0,0,0.1); }
        nav a { margin-right: 15px; text-decoration:none; }
        form label { display:block; margin-top:10px; font-weight:bold; }
        input[type=text], input[type=date], textarea { width:100%; padding:8px; margin-top:4px; }
        button { padding:6px 12px; margin-top:10px; }
        table { width:100%; border-collapse: collapse; margin-top:10px; }
        th, td { border:1px solid #ddd; padding:8px; text-align:left; }
        .timeline { position: relative; overflow-x: auto; padding-top: 40px; white-space: nowrap; }
        .timeline .header .cell { position: absolute; top:0; width:80px; text-align:center; font-size:12px; border-right:1px solid #ccc; }
        .timeline .projects { position: relative; height:30px; }
        .timeline .project { position:absolute; top:0; background:#2196F3; color:#fff; padding:4px; border-radius:4px; text-decoration:none; }
    </style>
</head>
<body>
<div class="container">
<nav><a href="{{ url_for('index') }}">Start</a></nav>
{{ content|safe }}
</div>
</body>
</html>'''


def render_page(title, body_template, **context):
    body = render_template_string(body_template, **context)
    return render_template_string(BASE_PAGE, title=title, content=Markup(body))


@app.route('/')
def index():
    data = load_data()
    scale = request.args.get('scale', 'month')
    if scale not in {'day', 'week', 'month'}:
        scale = 'month'

    projects = [p for p in data['AllProjects'] if p.get('StartDate') and p.get('EndDate')]
    from datetime import datetime, timedelta

    def parse(date):
        return datetime.fromisoformat(date)

    if projects:
        start = min(parse(p['StartDate']) for p in projects)
        end = max(parse(p['EndDate']) for p in projects)
    else:
        start = end = datetime.today()

    headers = []
    positions = []
    slot_width = {'day': 20, 'week': 40, 'month': 80}[scale]

    cur = start.replace(day=1) if scale == 'month' else start
    index = 0
    while cur <= end:
        if scale == 'day':
            label = cur.strftime('%Y-%m-%d')
            cur += timedelta(days=1)
        elif scale == 'week':
            label = f"v{cur.isocalendar().week} {cur.year}"
            cur += timedelta(days=7)
        else:
            label = cur.strftime('%b %Y')
            year = cur.year + (cur.month // 12)
            month = cur.month % 12 + 1
            cur = cur.replace(year=year, month=month, day=1)
        headers.append({'label': label, 'offset': index * slot_width})
        index += 1

    def offset(start_date):
        if scale == 'day':
            delta = (parse(start_date) - start).days
        elif scale == 'week':
            delta = (parse(start_date) - start).days // 7
        else:
            delta = (parse(start_date).year - start.year) * 12 + parse(start_date).month - start.month
        return delta * slot_width

    def width(s, e):
        if scale == 'day':
            delta = (parse(e) - parse(s)).days + 1
        elif scale == 'week':
            delta = ((parse(e) - parse(s)).days // 7) + 1
        else:
            delta = ((parse(e).year - parse(s).year) * 12 + parse(e).month - parse(s).month) + 1
        return max(delta * slot_width - 4, slot_width)

    for p in projects:
        positions.append({
            'title': p['Title'],
            'id': p['Id'],
            'offset': offset(p['StartDate']),
            'width': width(p['StartDate'], p['EndDate'])
        })

    body = '''
<h1>Projekt</h1>
<a href="{{ url_for('add_project') }}">Nytt projekt</a>
<p>Zooma:
  <a href="{{ url_for('index', scale='day') }}">Dag</a> |
  <a href="{{ url_for('index', scale='week') }}">Vecka</a> |
  <a href="{{ url_for('index', scale='month') }}">Månad</a>
</p>
<div class="timeline">
  <div class="header">
    {% for h in headers %}
    <div class="cell" style="left:{{ h['offset'] }}px">{{ h['label'] }}</div>
    {% endfor %}
  </div>
  <div class="projects">
    {% for p in positions %}
    <a class="project" href="{{ url_for('view_project', pid=p['id']) }}" style="left:{{ p['offset'] }}px;width:{{ p['width'] }}px">{{ p['title'] }}</a>
    {% endfor %}
  </div>
</div>
'''
    return render_page('Projekt', body, headers=headers, positions=positions)


@app.route('/project/add', methods=['GET', 'POST'])
def add_project():
    if request.method == 'POST':
        data = load_data()
        new_project = {
            'Id': str(uuid.uuid4()),
            'Title': request.form.get('title', '').strip(),
            'Description': request.form.get('description', '').strip(),
            'StartDate': request.form.get('start', ''),
            'EndDate': request.form.get('end', ''),
            'Row': 0,
            'AttachedFilePaths': [],
            'Tasks': []
        }
        data['AllProjects'].append(new_project)
        save_data(data)
        return redirect(url_for('index'))

    body = '''
<h1>Nytt projekt</h1>
<form method="post">
    <label for="title">Namn</label>
    <input id="title" name="title" type="text">
    <label for="description">Beskrivning</label>
    <textarea id="description" name="description"></textarea>
    <label for="start">Startdatum</label>
    <input id="start" type="date" name="start">
    <label for="end">Slutdatum</label>
    <input id="end" type="date" name="end">
    <button type="submit">Spara</button>
</form>
<a href="{{ url_for('index') }}">Tillbaka</a>
'''
    return render_page('Nytt projekt', body)


@app.route('/project/<pid>')
def view_project(pid):
    data = load_data()
    project = next((p for p in data['AllProjects'] if p['Id'] == pid), None)
    if not project:
        abort(404)
    body = '''
<h1>{{ p['Title'] }}</h1>
<p>{{ p['Description'] }}</p>
<p>{{ p['StartDate'] }} - {{ p['EndDate'] }}</p>
<a href="{{ url_for('add_task', pid=p['Id']) }}">Ny uppgift</a>
<table>
  <tr><th>Uppgifter</th></tr>
  {% for t in p['Tasks'] %}
  <tr>
    <td><a href="{{ url_for('view_task', pid=p['Id'], tid=t['Id']) }}">{{ t['Title'] }}</a></td>
  </tr>
  {% endfor %}
</table>
<a href="{{ url_for('index') }}">Tillbaka</a>
'''
    return render_page(project['Title'], body, p=project)


@app.route('/project/<pid>/task/add', methods=['GET', 'POST'])
def add_task(pid):
    data = load_data()
    project = next((p for p in data['AllProjects'] if p['Id'] == pid), None)
    if not project:
        abort(404)

    if request.method == 'POST':
        task = {
            'Id': str(uuid.uuid4()),
            'Title': request.form.get('title', '').strip(),
            'AssignedTo': [],
            'Parts': [],
            'Comments': []
        }
        project['Tasks'].append(task)
        save_data(data)
        return redirect(url_for('view_project', pid=pid))

    body = '''
<h1>Ny uppgift</h1>
<form method="post">
    <label for="title">Namn</label>
    <input id="title" name="title" type="text">
    <button type="submit">Spara</button>
</form>
<a href="{{ url_for('view_project', pid=pid) }}">Tillbaka</a>
'''
    return render_page('Ny uppgift', body, pid=pid)


@app.route('/project/<pid>/task/<tid>', methods=['GET', 'POST'])
def view_task(pid, tid):
    data = load_data()
    project = next((p for p in data['AllProjects'] if p['Id'] == pid), None)
    if not project:
        abort(404)
    task = next((t for t in project['Tasks'] if t['Id'] == tid), None)
    if not task:
        abort(404)

    if request.method == 'POST':
        part_title = request.form.get('part', '').strip()
        if part_title:
            part = {
                'Id': str(uuid.uuid4()),
                'Title': part_title,
                'IsCompleted': False,
                'Description': ''
            }
            task['Parts'].append(part)
            save_data(data)
        return redirect(url_for('view_task', pid=pid, tid=tid))

    body = '''
<h1>{{ task['Title'] }}</h1>
<table>
  <tr><th>Delmoment</th><th>Status</th></tr>
  {% for part in task['Parts'] %}
  <tr>
    <td>{{ part['Title'] }}</td>
    <td>{% if part['IsCompleted'] %}✓{% endif %}</td>
  </tr>
  {% endfor %}
</table>
<form method="post">
    <label for="part">Nytt delmoment</label>
    <input id="part" name="part" type="text">
    <button type="submit">Lägg till</button>
</form>
<a href="{{ url_for('view_project', pid=project['Id']) }}">Tillbaka</a>
'''
    return render_page(task['Title'], body, project=project, task=task)


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
