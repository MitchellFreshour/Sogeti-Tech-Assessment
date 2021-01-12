# api/tests/test_health.py

import json

def test_ping(test_app):
    # given
    client = test_app.test_client()

    # when
    resp = client.get('/health')
    data = json.loads(resp.data.decode())

    # then
    assert resp.status_code == 200
    assert 'You keep using that word. I do not think it means what you think it means.' in data['message']
    assert 'success' in data['status']