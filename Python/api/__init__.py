# api/__init__.py

from flask import Flask, jsonify
from flask_restx import Resource, Api

# create an instantiation of the Flask app
app = Flask(__name__)

api = Api(app)

# set environment config here
app.config.from_object('project.config.DevelopmentConfig')

class Health(Resource):
    def get(self):
        return {
            'status': 'success',
            'message': 'You keep using that word. I do not think it means what you think it means.'
        }

api.add_resource(Health, '/health')