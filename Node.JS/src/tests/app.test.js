const request = require('supertest');
const app = require('../app');

test('Should return 200 status', async () => {
    const response = await request(app).get('/health');
    expect(response.statusCode).toBe(200);
});

test('Test response text', async () => {
    const response = await request(app).get('/health');
    expect(response.text).toBe('You keep using that word. I do not think it means what you think it means.');
})
