ping = require('tcp-ping');

var pingOptions = { address: "10.200.2.153",
                    port: 80,
                    attempts: 2
                  }

ping.ping(pingOptions, function(err, data) {
        console.log(data.avg);
    });  