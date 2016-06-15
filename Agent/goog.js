if (!Date.prototype.toISOString) {
    Date.prototype.toISOString = function () {
        function pad(n) { return n < 10 ? '0' + n : n; }
        function ms(n) { return n < 10 ? '00'+ n : n < 100 ? '0' + n : n }
        return this.getFullYear() + '-' +
            pad(this.getMonth() + 1) + '-' +
            pad(this.getDate()) + 'T' +
            pad(this.getHours()) + ':' +
            pad(this.getMinutes()) + ':' +
            pad(this.getSeconds()) + '.' +
            ms(this.getMilliseconds()) + 'Z';
    }
}


var HAR = require('./har');
var Nightmare = require('nightmare');
var fs = require('fs');
var totalSize = 0;

var page = {title: "Test Title", address: "http://www.yahoo.com"};
    page.resources = [];
    page.times = new Object();
    page.times.startTime = [];
    page.times.endTime = [];
    page.times.url = [];
    
new Nightmare({ timeout: 60000 })
  .on('resourceReceived', function (res) {
        if (res.stage === 'start') {
            page.resources[res.id].startReply = res;
            totalSize += res.bodySize;
        }
        if (res.stage === 'end') {
            page.resources[res.id].endReply = res;
        }
    })
  .on('resourceRequested', function (req) {
        page.resources[req.id] = {
            request: req,
            startReply: null,
            endReply: null
        };
    })
  .on('loadStarted', function () {
        page.startTime = new Date();
        page.times.startTime.push(new Date());
        // make startTime and endTime arrays
        // check if endTime < next request time, create new HAR group
    })
  .on('loadFinished', function () {
        page.endTime = new Date();
        page.times.endTime.push(new Date());
    })
  .on('urlChanged', function() {
        page.times.url.push(new Date());
    })
  .goto('https://www.google.com')
    .screenshot('./test.jpg')
    .type('#lst-ib', 'funny cat pictures')
    .wait()
    .screenshot('./test1.jpg')
    .run(function (err, nightmare) {
      if (err) {
        return console.log(err);
      } else {
            var pages = [];
            for (var q = 0; q < page.times.startTime.length; q++)
            {
                pages.push({
                    startTime: page.times.startTime[q],
                    endTime: page.times.endTime[q],
                    id: page.address,
                    title: page.title
                });
            }
            
            page.startTime = page.times.startTime[0];
            har = HAR.createHAR(pages, "https://www.google.com", "Gloobal", page.startTime, page.resources);
            //console.log(JSON.stringify(har, undefined, 4));
            //console.log("Total page load started = " + page.times.start.length);
            fs.writeFile("./test.har", JSON.stringify(har, undefined, 4), function(err) { if (err) { return console.log("File write error") } });
            for (var j = 0; j < page.times.startTime.length; j++)
            {
                console.log("Start: " + page.times.startTime[j]);
                console.log("End: " + page.times.endTime[j]);
                console.log("Url: " + page.times.url[j]);
                console.log("Total time to load = " + ((new Date(page.times.endTime[j])) - (new Date(page.times.startTime[j]))));
                console.log("Body size: " + totalSize);
            }
      }
    });