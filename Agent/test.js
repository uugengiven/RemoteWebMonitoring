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
var ping = require('tcp-ping');
var postdata = require('./postdata');
var config = require('./config');
var har;

var currentPageid = -1;
var totalSize = 0;
var postId = 0; 
var page = {title: "Test Title", address: "http://www.yahoo.com"};
    page.resources = [];
    page.times = new Object();
    page.times.startTime = [];
    page.times.endTime = [];
    page.times.url = [];
    
    var data = {
    nodeId: config.id,
    nodeName: config.name,
    testTime: new Date(),
    ip: config.ip,
    dns: config.dns,
    geoFrom: config.geoFrom,
    geoTo: config.geoTo,
    testId: 1,
    ping: 0,
    totalTime: 0,
    totalSize: 0
    }            

    var pingOptions = { address: data.ip,
                        port: 80,
                        attempts: 2
                      }

    ping.ping(pingOptions, function(err, res) {
            data.ping = Math.round(res.avg);
        });        
    
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
            endReply: null,
            pageref: "page-" + currentPageid
        };
    })
  .on('loadStarted', function () {
        page.startTime = new Date();
        page.times.startTime.push(new Date());
        currentPageid++;
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
  .goto('https://qa.admin2.talentportal.ddiworld.com')
    .type('#UserName', 'automation@test.com')
    .type('#Password', 'automation1')
    .screenshot('./test.jpg')
    .click('#logon-button')
    .wait("Dashboard Home")
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
                    id: "page-" + q,
                    title: page.title + q
                });
            }
            
            var totTime = 0;
            for (var j = 0; j < page.times.startTime.length; j++)
            {
                console.log("Start: " + page.times.startTime[j]);
                console.log("End: " + page.times.endTime[j]);
                console.log("Url: " + page.times.url[j]);
                totTime += ((new Date(page.times.endTime[j])) - (new Date(page.times.startTime[j])));
                console.log("Total time to load = " + ((new Date(page.times.endTime[j])) - (new Date(page.times.startTime[j]))));
                console.log("Total pages started " + currentPageid);
            }
            
            data.totalTime = totTime;
            data.totalSize = totalSize;
            page.startTime = page.times.startTime[0];
            har = HAR.createHAR(pages, "https://www.google.com", "Gloobal", page.startTime, page.resources);   
            
            
            console.log(JSON.stringify(data, undefined, 4));
            postdata.post("distributedwebtest.azurewebsites.net", "/api/data/savetestresults", data, function (res) { 
                postId = res; 
                fs.writeFile("./test" + postId + ".har", JSON.stringify(har, undefined, 4), function(err) { if (err) { return console.log("File write error") } });
                console.log(res);
                console.log(postId);
                var harData = new Object();
                harData.id = postId;
                harData.file = JSON.stringify(har);
                
                //fs.writeFile("./test" + postId + ".har", JSON.stringify(harData, undefined, 4), function(err) { if (err) { return console.log("File write error") } });

//                console.log(JSON.stringify(harData, undefined, 4));
                postdata.post("distributedwebtest.azurewebsites.net", "/api/data/savehar", harData, function (res) { console.log(res); } );               
            });

            



            

      }
    });