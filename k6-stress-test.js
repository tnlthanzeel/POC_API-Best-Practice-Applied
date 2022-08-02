import http from 'k6/http';
import { check } from 'k6';


//export const requests = new Counter('http_reqs');

// you can specify stages of your test (ramp up/down patterns) through the options object
// target is the number of VUs you are aiming for

export const options = {
    vus: 10,
    duration: '10m',
    insecureSkipTLSVerify: true
};

export default function() {
    // our HTTP request, note that we are saving the response to res, which can be accessed later

    const url = 'https://localhost:44379/api/todo-item';

    const payload = JSON.stringify({
        "title": "string",
        "description": "string"
    });

    const params = {
        headers: {
            'Content-Type': 'application/json'
        }
    };

    const res = http.post(url, payload, params);

    check(res, { 'is status 201': (r) => r.status === 201 });
}