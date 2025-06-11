import os
import json
import uuid
from flask import Flask, request, redirect, url_for, render_template_string, abort

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


@app.route('/')
def index():
    data = load_data()
    return render_template_string('''
    <h1>Projects</h1>
    <a href="{{ url_for('add_project') }}">Add project</a>
    <ul>
    {% for p in projects %}
      <li><a href="{{ url_for('view_project', pid=p['Id']) }}">{{ p['Title'] }}</a>
          ({{ p['StartDate'] }} - {{ p['EndDate'] }})
      </li>
    {% endfor %}
    </ul>
    ''', projects=data['AllProjects'])


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

    return render_template_string('''
        <h1>New Project</h1>
        <form method="post">
            Title: <input name="title"><br>
            Description:<br>
            <textarea name="description"></textarea><br>
            Start date: <input type="date" name="start"><br>
            End date: <input type="date" name="end"><br>
            <button type="submit">Save</button>
        </form>
        <a href="{{ url_for('index') }}">Back</a>
    ''')


@app.route('/project/<pid>')
def view_project(pid):
    data = load_data()
    project = next((p for p in data['AllProjects'] if p['Id'] == pid), None)
    if not project:
        abort(404)
    return render_template_string('''
        <h1>{{ p['Title'] }}</h1>
        <p>{{ p['Description'] }}</p>
        <p>{{ p['StartDate'] }} - {{ p['EndDate'] }}</p>
        <a href="{{ url_for('add_task', pid=p['Id']) }}">Add task</a>
        <ul>
        {% for t in p['Tasks'] %}
            <li><a href="{{ url_for('view_task', pid=p['Id'], tid=t['Id']) }}">{{ t['Title'] }}</a></li>
        {% endfor %}
        </ul>
        <a href="{{ url_for('index') }}">Back</a>
    ''', p=project)


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

    return render_template_string('''
        <h1>New Task</h1>
        <form method="post">
            Title: <input name="title"><br>
            <button type="submit">Save</button>
        </form>
        <a href="{{ url_for('view_project', pid=pid) }}">Back</a>
    ''', pid=pid)


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

    return render_template_string('''
        <h1>{{ task['Title'] }}</h1>
        <ul>
        {% for part in task['Parts'] %}
            <li>{{ part['Title'] }}{% if part['IsCompleted'] %} (done){% endif %}</li>
        {% endfor %}
        </ul>
        <form method="post">
            Add part: <input name="part">
            <button type="submit">Add</button>
        </form>
        <a href="{{ url_for('view_project', pid=project['Id']) }}">Back</a>
    ''', project=project, task=task)


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
