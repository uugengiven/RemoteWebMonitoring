// har.js
// ======

module.exports = {
    createHAR: function (page, address, title, startTime, resources){
        var entries = [];
        var pages = [];
        page.forEach(function (currPage) {
            pages.push({
                startedDateTime: (new Date(currPage.startTime)),
                id: currPage.id,
                title: currPage.title,
                pageTimings: {
                    onLoad: (new Date(currPage.endTime)) - (new Date(currPage.startTime))
                }
            });
        });
        resources.forEach(function (resource) {

            var request = resource.request,
                startReply = resource.startReply,
                endReply = resource.endReply;

            if (!request || !startReply || !endReply) {
                return;
            }

            // Exclude Data URI from HAR file because
            // they aren't included in specification
            //if (request.url.match(/(^data:image\/.*)/i)) {
            //    return;
            //}

            //console.log(JSON.stringify(resource, undefined, 4));
            entries.push({
                startedDateTime: new Date(request.time),
                time: (new Date(endReply.time)) - (new Date(request.time)),
                request: {
                    method: request.method,
                    url: request.url,
                    httpVersion: "HTTP/1.1",
                    cookies: [],
                    headers: request.headers,
                    queryString: [],
                    headersSize: -1,
                    bodySize: -1
                },
                response: {
                    status: endReply.status,
                    statusText: endReply.statusText,
                    httpVersion: "HTTP/1.1",
                    cookies: [],
                    headers: endReply.headers,
                    redirectURL: "",
                    headersSize: -1,
                    bodySize: startReply.bodySize,
                    content: {
                        size: startReply.bodySize,
                        mimeType: endReply.contentType ?  endReply.contentType : "text/plain"
                    }
                },
                cache: {},
                timings: {
                    blocked: 0,
                    dns: -1,
                    connect: -1,
                    send: 0,
                    wait: (new Date(startReply.time)) - (new Date(request.time)),
                    receive: (new Date(endReply.time)) - (new Date(startReply.time)),
                    ssl: -1
                },
                pageref: resource.pageref
            });
        });

        return {
            log: {
                version: '1.2',
                creator: {
                    name: "PhantomJS",
                    version: '2.0'
                },
                pages: pages,
                entries: entries
            }
        };
    }
};

