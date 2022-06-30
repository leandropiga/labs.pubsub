import { sleep, check } from "k6";
import http from "k6/http";
import { randomIntBetween, 
  randomItem, 
  uuidv4 } from "https://jslib.k6.io/k6-utils/1.1.0/index.js";

export const options = {
  thresholds: {
    // 90% of requests must finish within 400ms, 95% within 800, and 99.9% within 2s.
    http_req_duration: ['p(90) < 400', 'p(95) < 800', 'p(99.9) < 2000'],
     // During the whole test execution, the error rate must be lower than 1%.
    // `http_req_failed` metric is available since v0.31.0
    http_req_failed: ['rate<0.01'],
  },  
    stages: [
        { duration: '30s', target:  10 }, 
        { duration: '1m' , target:  50 },
        { duration: '2m' , target: 100 }, 
        { duration: '2m' , target: 200 },
        { duration: '1m' , target:  50 }, 
        { duration: '30s', target:  10 },
        // { duration: '2m', target: 200 },
        // { duration: '5m', target: 200 },
        // { duration: '2m', target: 50 }, 
    ],  

  ext: {
    loadimpact: {
      distribution: {
        "amazon:br:sao paulo": {
          loadZone: "amazon:br:sao paulo",
          percent: 100,
        },
      },
    },
  },
};


export default function main() {

  let response;
  let log;

  //const correlationId = uuidv4();
  //const customerId = uuidv4();
  const url = "https://labs-pubsub-api-nmq7pwb73a-uc.a.run.app/invoice";

  const payload = JSON.stringify(
    {
      "customerIdentification": "111.111.111-11",
      "customerName": "PubSub Labs",
      "issuanceDate": "2022-06-27T08:00:00Z",
      "total": 5000
    });



  const params = {
    headers: { 'Content-Type': 'application/json' },
  }
 
  // Request
  response = http.post(url, payload,  params);

  check(response, {
    "status OK": responseEmail => responseEmail.status === 200,      
    "response time < 900 ": responseEmail => responseEmail.timings.duration < 900,
  });

  // Automatically added sleep
  sleep(1);
}
