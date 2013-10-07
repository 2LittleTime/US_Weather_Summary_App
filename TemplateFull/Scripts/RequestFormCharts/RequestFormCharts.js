
function getDay(s1, s2) {

    var s1 = document.getElementById(s1);
    var s2 = document.getElementById(s2);

    s2.innerHTML = "";

    if (s1.value == "1" || s1.value == "3" || s1.value == "5" || s1.value == "7" || s1.value == "8" || s1.value == "10" || s1.value == "12") {
        var optionArray = numOptionArray(31);
    } else if (s1.value == "2") {
        var optionArray = numOptionArray(28);
    } else if (s1.value == "4" || s1.value == "6" || s1.value == "9" || s1.value == "11") {
        var optionArray = numOptionArray(30);
    }

    for (var i = 0; i < optionArray.length; i++) {
        var pair = optionArray[i].split("|");
        var newOption = document.createElement("option");
        newOption.value = pair[0];
        newOption.innerHTML = pair[1];
        s2.options.add(newOption);
    }

}



function numOptionArray(endVal) {

    var x = [];
    var pushString

    for (var i = 1; i <= endVal; i++) {
        pushString = i + '|' + i;
        x.push(pushString);
    }
  
    return x;
}


function getStationNames() {

    var e = document.getElementById("stationState");
    var state = e.options[e.selectedIndex].text;
    var sl = document.getElementById("stationName");
    sl.innerHTML = "";


    //build data request string 
    var sendToAjax = 'state=' + state;

    //make ajax call to average wind api
    $.ajax({
        url: ("/api/StationNameApi/GetStationNames"),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: sendToAjax,
        async: false,
        success: function (data) {
            //check if anything was returned
            if (data.length != 0) {

                //testing code ----------------
                //build testing visualizations for returned data
                //var table = '<table>';
                //var dataStream = '[';

                //build option elements
                $.each(data, function (index, item) {

                    //testing code ----------------
                    //add row data to testing visualization strings
                    //table += '<tr><td>' + item.WbanId + '</td><td>' + item.StationName + '</td><td>' + item.Location + '</td></tr>';
                    //dataStream += '{"StationName":' + item.StationName + ',"Location":' + item.Location + '},';

                    //add option elements to StationNames
                    var newOption = document.createElement("option");
                    newOption.value = item.WbanId;
                    newOption.innerHTML = item.StationName;
                    sl.options.add(newOption);
                    
                });

                //testing code ----------------
                //format testing visualizations
                //dataStream = dataStream.substring(0, dataStream.length - 1);
                //dataStream += ']';
                //var dataJSON = $.parseJSON(dataStream);

                // format and display data visualizations
                //table += '</table>';
                //var dataStreamTable;
                //dataStreamTable = '<table><tr><td>' + dataStream + '</td></tr></table>';

                // insert the html data visualizations strings
                //$("#sampleTable").html(table);
                //$("#sampleStream").html(dataStream);

            }
        },
        error: function (xhr, txt, err) {
            alert("error connection to avg wind api: " + txt);
        }

    });

}