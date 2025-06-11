# AdvancedToDoList

This repository originally contains a WinForms based project and task manager.

## Web version

A simplified web front end is available in `main.py` using [Flask](https://flask.palletsprojects.com/).
To start the web app, install Flask and run `main.py`:

```bash
pip install flask
python3 main.py
```

The application will store its data in `projects.json` in the repository folder and
serve on [http://localhost:5000](http://localhost:5000).

The HTML pages use simple styling inspired by the WinForms dialogs to make it easier to work with projects and tasks.