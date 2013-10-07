// create the chart objects
var tempChart;
var precipChart;
var avgWindChart;

// retrieve and validate the data from the input form
// send this data to the APIs if it passes validation 
function getData() {

    //retrieve the selections from the drop down lists
    var sS = document.getElementById("stationState");
    var stationState = sS.options[sS.selectedIndex].value;

    var sN = document.getElementById("stationName");
    var stationName = sN.options[sN.selectedIndex].value;

    var bM = document.getElementById("beginMonth");
    var beginMonth = bM.options[bM.selectedIndex].value;

    var bD = document.getElementById("beginDay");
    var beginDay = bD.options[bD.selectedIndex].value;

    var bY = document.getElementById("BeginYear");
    var beginYear = bY.options[bY.selectedIndex].value;

    var eM = document.getElementById("endMonth");
    var endMonth = eM.options[eM.selectedIndex].value;

    var eD = document.getElementById("endDay");
    var endDay = eD.options[eD.selectedIndex].value;

    var eY = document.getElementById("EndYear");
    var endYear = eY.options[eY.selectedIndex].value;
  
    //force all selection items to integers for validation
    var bMi = parseInt(beginMonth);
    var bDi = parseInt(beginDay);
    var bYi = parseInt(beginYear);
    var eMi = parseInt(endMonth);
    var eDi = parseInt(endDay);
    var eYi = parseInt(endYear);

    //create the query string to send to the APIs
    var dataToSend = 'wBan=' + stationName + '&beginMonth=' + beginMonth + '&beginDay=' + beginDay + '&beginYear=' + beginYear + '&endMonth=' + endMonth + '&endDay=' + endDay + '&endYear=' + endYear;
  
    //a fake variable for debugging and testing validation
    var bK1 = 1;

    //validation logic - combination of end month and day must be greater than or equal to the end month and day
    //validation logic - end year must be greater than or equal to the begin year
    //cannot us data datatype validation as this won't work with the day-range logic of the datasets
    if (eYi < bYi) {
        alert("The end year must be greater than or equal to the begin year.");
    } else if (eMi < bMi) {
        alert("The end month must be greater than or equal to the begin month.");
    } else if ((eMi == bMi) && (eDi < bDi)) {
        alert("The end day must be greater than or equal to the begin day.");
    }
    else {
        loadTempChart(dataToSend);
        loadWindChart(dataToSend);
        loadPrecipChart(dataToSend);
    }

}


function loadWindChart(sendToAjax) {

    //useful sample data
    var data2 = [
        { "DateId": 186, "Month": 7, "Day": 5, "WindAvg": 6.670 },
        { "DateId": 187, "Month": 7, "Day": 6, "WindAvg": 5.590 },
        { "DateId": 188, "Month": 7, "Day": 7, "WindAvg": 5.810 },
        { "DateId": 189, "Month": 7, "Day": 8, "WindAvg": 7.460 },
        { "DateId": 190, "Month": 7, "Day": 9, "WindAvg": 8.410 },
        { "DateId": 191, "Month": 7, "Day": 10, "WindAvg": 7.740 }
    ];


    //use in debugger to see query string
    var sendToAjax2 = sendToAjax

    //make ajax call to average wind api
    $.ajax({
        url: ("/api/AvgWindApi"),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: sendToAjax,
        async: true,
        success: function (data) {
            //check if anything was returned
            if (data.length != 0) {

                //clear out previous chart
                var curChart = document.getElementById("windChart");
                curChart.innerHTML = "";

                //build visualizations for returned data
                var table = '<table>';
                var dataStream = '[';

                //loop over each object in the array to create rows
                //build min-max x and y chart values
                var minX = 1000000;
                var maxX = 0;
                var minY = 0;
                var maxY = 0;

                //create chart data array
                var windArray = new Array();

                //build data visualization strings
                $.each(data, function (index, item) {

                    //testing code ----------------
                    //testing -- add row data to exta visualization strings
                    //table += '<tr><td>' + item.DateId + '</td><td>' + item.WindAvg + '</td></tr>';
                    //dataStream += '{"DateId":' + item.DateId + ',"WindAvg":' + item.WindAvg + ',"XAxis":"' + item.Month + '-' + item.Day + '"},';
                    //dataStream += '{"XAxis":"2013-' + item.Month + '-' + item.Day + 'T23:45:10.280Z","WindAvg":' + item.WindAvg + '},';

                    //create chart data item - add to chart array
                    var avgWindObs = new Object();
                    var jsMonth = parseInt(item.Month) - 1;
                    avgWindObs["Date"] = new Date(2013, jsMonth, item.Day, 0, 0, 0, 0);
                    avgWindObs["AvgWindSpeed"] = item.WindAvg;
                    windArray.push(avgWindObs);

                    //set minX value
                    if (item.DateId < minX) {
                        minX = item.DateId;
                    }

                    //set maxX value
                    if (item.DateId > maxX) {
                        maxX = item.DateId;
                    }

                    //set maxY value
                    if (item.WindAvg > maxY) {
                        maxY = item.WindAvg;
                    }

                    //leave minY value as 0 for now.............
                    var breakPoint = 0;

                });

                //testing code ----------------
                //remove last comma
                //dataStream = dataStream.substring(0, dataStream.length - 1);
                //dataStream += ']';
                //var dataJSON = $.parseJSON(dataStream);

                //create chart object
                avgWindChart = new cfx.Chart();

                //set x axis
                var axisX = avgWindChart.getAxisX();
                axisX.getLabelsFormat().setFormat(cfx.AxisFormat.Date);
                axisX.getLabelsFormat().setCustomFormat("MMM-dd");

                //set y axis - let y axis autoformat based on dates supplied
                //keep this code for now if needed later
                //var axisY = avgWindChart.getAxisY();
                //axisY.setMin(0);
                //axisY.setMax(maxY + 1);

                //set field mappings
                var fields = avgWindChart.getDataSourceSettings().getFields();

                

                //set field1 mappings
                var field1 = new cfx.FieldMap();
                field1.setName("AvgWindSpeed");
                field1.setUsage(cfx.FieldUsage.Value);
                fields.add(field1);

                //set field2 mappings
                var field2 = new cfx.FieldMap();
                field2.setName("Date");
                field2.setUsage(cfx.FieldUsage.XValue);
                fields.add(field2);

                //set chart gallery type - lines
                avgWindChart.setGallery(cfx.Gallery.Lines);

                //set chart data source
                avgWindChart.setDataSource(windArray);

                //map chart to view div
                var windChartHolder = document.getElementById('windChart');

                //set line color
                //avgWindChart.getSeries().getItem(0).setColor("#FFA500");

                //create the chart
                avgWindChart.create(windChartHolder);

                //testing code -------------------
                // format and display data visualizations
                //table += '</table>';
                //var dataStreamTable;
                //dataStreamTable = '<table><tr><td>' + dataStream + '</td></tr></table>';

                //testing code -------------------
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


function loadPrecipChart(sendToAjax) {

    //make ajax call to average precip api
    $.ajax({
        url: ("/api/PrecipApi"),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: sendToAjax,
        async: true,
        success: function (data) {
            //check if anything was returned
            if (data.length != 0) {

                //clear out previous chart
                var curChart = document.getElementById("precipChart");
                curChart.innerHTML = "";

                //loop over each object in the array to create rows
                //build min-max x and y chart values
                var minX = 1000000;
                var maxX = 0;
                var minY = 0;
                var maxY = 0;

                //create chart data array
                var precipArray = new Array();

                //build data visualization strings
                $.each(data, function (index, item) {

                    //create chart data item - add to chart array
                    var avgPrecipObs = new Object();
                    var jsMonth = parseInt(item.Month) - 1;
                    avgPrecipObs["Date"] = new Date(2013, jsMonth, item.Day, 0, 0, 0, 0);
                    avgPrecipObs["AvgPrecipInches"] = item.PrecipAvg;
                    precipArray.push(avgPrecipObs);

                    //set minX value
                    if (item.DateId < minX) {
                        minX = item.DateId;
                    }

                    //set maxX value
                    if (item.DateId > maxX) {
                        maxX = item.DateId;
                    }

                    //set maxY value
                    if (item.PrecipAvg > maxY) {
                        maxY = item.PrecipAvg;
                    }

                    //leave minY value as 0 for now.............
                    var breakPoint = 0;

                });

                //create chart object
                precipChart = new cfx.Chart();

                //set x axis
                var axisX = precipChart.getAxisX();
                axisX.getLabelsFormat().setFormat(cfx.AxisFormat.Date);
                axisX.getLabelsFormat().setCustomFormat("MMM-dd");

                //set y axis
                var axisY = precipChart.getAxisY();
                axisY.setMin(0);
                axisY.setMax(maxY + .1);
                axisY.setStep(.1);
                axisY.getLabelsFormat().setDecimals(2);

                //set field mappings
                var fields = precipChart.getDataSourceSettings().getFields();

                

                //set field1 mappings
                var field1 = new cfx.FieldMap();
                field1.setName("AvgPrecipInches");
                field1.setUsage(cfx.FieldUsage.Value);
                fields.add(field1);

                //set field2 mappings
                var field2 = new cfx.FieldMap();
                field2.setName("Date");
                field2.setUsage(cfx.FieldUsage.XValue);
                fields.add(field2);

                //set chart gallery type - lines
                precipChart.setGallery(cfx.Gallery.Lines);

                //set chart data source
                precipChart.setDataSource(precipArray);

                //map chart to view div
                var precipChartHolder = document.getElementById('precipChart');

                //set line color
                //precipChart.getSeries().getItem(0).setColor("#00008B");

                //create the chart
                precipChart.create(precipChartHolder);

            }
        },
        error: function (xhr, txt, err) {
            alert("error connection to avg precip api: " + txt);
        }

    });

}

function loadTempChart(sendToAjax) {

    //make ajax call to average temp api
    $.ajax({
        url: ("/api/TempApi"),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: sendToAjax,
        async: true,
        success: function (data) {
            //check if anything was returned
            if (data.length != 0) {

                //clear out previous chart
                var curChart = document.getElementById("tempChart");
                curChart.innerHTML = "";

                //loop over each object in the array to create rows
                //build min-max x and y chart values
                var minX = 1000000;
                var maxX = 0;
                var minY = 1000000;
                var maxY = 0;

                //create chart data array
                var tempArray = new Array();

                //build data visualization strings
                $.each(data, function (index, item) {

                    //create chart data item - add to chart array
                    var avgTempObs = new Object();
                    var jsMonth = parseInt(item.Month) - 1;
                    avgTempObs["Date"] = new Date(2013, jsMonth, item.Day, 0, 0, 0, 0);
                    avgTempObs["MaxTemp"] = item.AvgTempMax;
                    avgTempObs["AvgTemp"] = item.AvgTempAvg;
                    avgTempObs["MinTemp"] = item.AvgTempMin;
                    tempArray.push(avgTempObs);

                    //set minX value
                    if (item.DateId < minX) {
                        minX = item.DateId;
                    }

                    //set maxX value
                    if (item.DateId > maxX) {
                        maxX = item.DateId;
                    }

                    //set maxY value
                    if (item.AvgTempMax > maxY) {
                        maxY = item.AvgTempMax;
                    }

                    //set minY value
                    if (item.AvgTempMin < minY) {
                        minY = item.AvgTempMin;
                    }

                });

                //create chart object
                tempChart = new cfx.Chart();

                //set x axis
                var axisX = tempChart.getAxisX();
                axisX.getLabelsFormat().setFormat(cfx.AxisFormat.Date);
                axisX.getLabelsFormat().setCustomFormat("MMM-dd");

                //set y axis 
                var axisY = tempChart.getAxisY();
                axisY.setMin(minY - 10);
                axisY.setMax(maxY + 10);
                axisY.setStep(10);
                axisY.getLabelsFormat().setDecimals(1);

                //set series numbers
                var cData = tempChart.getData();
                cData.setSeries(3);
                tempChart.getSeries().getItem(0).setMarkerShape(cfx.MarkerShape.VerticalLine);
                tempChart.getSeries().getItem(1).setMarkerShape(cfx.MarkerShape.Marble);
                tempChart.getSeries().getItem(2).setMarkerShape(cfx.MarkerShape.InvertedTriangle);
                //tempChart.getSeries().getItem(0).setColor("#FF0000");
                //tempChart.getSeries().getItem(1).setColor("#DAA520");
                //tempChart.getSeries().getItem(2).setColor("#0000FF");

                //var series1 = tempChart.getSeries().getItem(0);
                //series1.setMarkerShape(cfx.MarkerShape.InvertedTriangle);
                //series1.setColor("#FF0000");

                //set field mappings
                var fields = tempChart.getDataSourceSettings().getFields();

                //set field1 mappings
                var field1 = new cfx.FieldMap();
                field1.setName("MinTemp");
                field1.setUsage(cfx.FieldUsage.Value);
                fields.add(field1);

                //set field2 mappings
                var field2 = new cfx.FieldMap();
                field2.setName("AvgTemp");
                field2.setUsage(cfx.FieldUsage.Value);
                fields.add(field2);

                //set field3 mappings
                var field3 = new cfx.FieldMap();
                field3.setName("MaxTemp");
                field3.setUsage(cfx.FieldUsage.Value);
                fields.add(field3);

                //set field4 mappings
                var field4 = new cfx.FieldMap();
                field4.setName("Date");
                field4.setUsage(cfx.FieldUsage.XValue);
                fields.add(field4);

                //set chart gallery type - lines
                tempChart.setGallery(cfx.Gallery.Lines);

                //set chart data source
                tempChart.setDataSource(tempArray);

                //map chart to view div
                var tempChartHolder = document.getElementById('tempChart');

                //set colors
                

                //create the chart
                tempChart.create(tempChartHolder);

            }
        },
        error: function (xhr, txt, err) {
            alert("error connection to avg temperature api: " + txt);
        }

    });

}


