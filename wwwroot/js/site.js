$(function () {
    var i = 0;
    $("#command").change(function () {
        var RawData = $("#command option:selected").attr('data-raw');        
        var Data = JSON.parse(RawData);
        $("#out").empty();
        if (Data.parameter_name1 != null) {        
            $("#out").prepend(`<label>${Data.parameter_name1}</label><input value=${Data.parameter_default_value1} class="form-control"/>`)
        }
        if (Data.parameter_name2 != null) {
            $("#out").prepend(`<label>${Data.parameter_name2}</label><input value=${Data.parameter_default_value2} class="form-control"/>`)
        }
        if (Data.parameter_name3 != null) {
            $("#out").prepend(`<label>${Data.parameter_name3}</label><input value=${Data.parameter_default_value3} class="form-control"/>`)
        }
        if (Data.parameter_name4 != null) {
            $("#out").prepend(`<label>${Data.parameter_name4}</label><input value=${Data.parameter_default_value4} class="form-control"/>`)
        }       
    })

    $("#send").click(function () {
        var parameter_1 = $("#out input").eq(0);
        if (parameter_1.length != 0) parameter_1 = parameter_1.val(); else parameter_1 = "0";
        var parameter_2 = $("#out input").eq(1);
        if (parameter_2.length != 0) parameter_2 = parameter_2.val(); else parameter_2 = "0";
        var parameter_3 = $("#out input").eq(2);
        if (parameter_3.length != 0) parameter_3 = parameter_3.val(); else parameter_3 = "0";
        var parameter_4 = $("#out input").eq(3);
        if (parameter_4.length != 0) parameter_4 = parameter_4.val(); else parameter_4 = "0";
        var Token = $("#command option:selected").attr('data-token');
        $.ajax({
            url: "api/terminal/sendtoterminal",
            method: "post",
            data: {
                "command_id": $("#command").val(),
                "parameter_1": parameter_1,
                "parameter_2": parameter_2,
                "parameter_3": parameter_3,
                "parameter_4": parameter_4,
                "terminal": $("#terminal").val(),
                "token": Token
            },
            success: function (r) {
                var res = JSON.parse(r);
                if (res.hasOwnProperty("error")) {
                    alert(res.error);
                    return;
                }
                console.table(res);
                i++;
                var command = $("#command option:selected").text();
                $("#result-table tbody").prepend(`
                    <tr>
                        <td>${i}</td>
                        <td>${res.item.time_created}</td>
                        <td>${command}</td>
                        <td>${parameter_1}</td>
                        <td>${parameter_2}</td>
                        <td>${parameter_3}</td>
                        <td>${res.item.state_name}</td>
                    </tr>
                    `);
            }
        })
    })
})