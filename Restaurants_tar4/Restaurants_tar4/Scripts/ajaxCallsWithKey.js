function ajaxCall(method, api, data, successCB, errorCB) {
    $.ajax({
        type: method,
        url: api,
        data: data,
        cache: false,
		headers: {
            'user-key': '65345f8da2e0c83fb79f7fb00db014d4'
        },
        contentType: "application/json",
        dataType: "json",
        success: successCB,
        error: errorCB
    });
}