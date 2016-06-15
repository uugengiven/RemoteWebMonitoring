// postdata.js for posting to the api
// ==================================

module.exports = {
    post: function (host, endpoint, data, success) {
    
        var querystring = require('querystring');
        var http = require('http');
        var method = "POST";
        var dataString = JSON.stringify(data);
      var headers = {};
      
      if (method == 'GET') {
        endpoint += '?' + querystring.stringify(data);
      }
      else {
        headers = {
          'Content-Type': 'application/json',
          'Content-Length': dataString.length
        };
      }
      var options = {
        host: host,
        path: endpoint,
        method: method,
        headers: headers
      };

      var req = http.request(options, function(res) {
        res.setEncoding('utf-8');

        var responseString = '';

        res.on('data', function(data) {
          responseString += data;
        });

        res.on('end', function() {
          console.log(responseString);
          if(responseString){
            var responseObject = JSON.parse(responseString);
            success(responseObject);
          }
            else
          {
            success();
          }
        });
      });

      req.write(dataString);
      req.end();
    }
}
