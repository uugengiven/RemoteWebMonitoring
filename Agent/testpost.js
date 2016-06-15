var postdata = require('./postdata');

var data = {
    nodeId: "df75afc9-582d-4fd8-92e8-c6bdc06b502b",
    nodeName: "Nodesic",
    testTime: new Date(),
    ip: "198.233.179.5",
    dns: "10.200.2.153",
    geoFrom: {
        latitude: "37.7833",
        longitude: "-122.4167"
    },
    geoTo: {
        latitude: "40.3569",
        longitude: "-80.1086"
    },
    testId: 1,
    ping: 1000,
    totalTime: 4000,
    totalSize: 1200000
}
        
postdata.post("distributedwebtest.azurewebsites.net", "/api/data/savetestresults", data, function (res) { console.log(res); } );

console.log("done?");