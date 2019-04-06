$(document).ready(function () {
    $('#detailsButton').click(function () {
        var attr = $("#tableIdReport").attr('style');
        if (typeof attr !== typeof undefined && attr !== false) {
            $('#tableIdReport').removeAttr("style");
            $(this).val('Hide Details');
        }
        else {
            $('#tableIdReport').attr('style', 'display:none');
            $(this).val('Show Details');
        }
    })
});
$(document).ready(function () {
    let hour = 0;
    let minute = 0;
    let seconds = 0;
    let totalSeconds = 0;
    let intervalId = null;
    let dateStarted = new Date();
    let userId = $('#userId').text();
    if (window.localStorage.getItem(userId)) {
        $('#commentId').removeAttr("style");
        $('#clockInButton').disabled = true;
        $('#clockOutButton').removeAttr("disabled");
        var savedStartDateLocal = window.localStorage.getItem(userId);
        var savedStartDate = new Date(savedStartDateLocal);
        var currentDate = new Date();
        var savedStartDateMS = savedStartDate.getTime();
        var currentDateMS = currentDate.getTime();
        var differenceMS = currentDateMS - savedStartDateMS;
        totalSeconds = parseInt(differenceMS / 1000);
        dateStarted = savedStartDate;
        intervalId = setInterval(startTimer, 1000);
    }
    else {
        $('#clockOutButton').disabled = true;
        $('#clockInButton').removeAttr("disabled");
    }


    $('#clockInButton').click(function () {
        this.disabled = true;
        $('#clockOutButton').removeAttr("disabled");
        $('#commentId').removeAttr("style");
        intervalId = setInterval(startTimer, 1000);
        dateStarted = new Date();
    })
    $('#clockOutButton').click(function () {
        var comment = $('#commentId').val();
        $('#commentId').val("");
        this.disabled = true;
        $('#clockInButton').removeAttr("disabled");
        $('#commentId').attr("style", "display:none");
        if (intervalId)
            clearInterval(intervalId);
        $.ajax({
            url: '/Timesheet/PostDate',
            data: {
                hour: hour,
                minute: minute,
                seconds: seconds,
                comment: comment
            },
            dataType: 'json',
            method: 'post',
            success: function (data) {
                $('#tableId tbody').append("<tr><td>" + dateToTime(data.Timesheet.ClockInTime) + "</td><td>" + dateToTime(data.Timesheet.ClockOutTime) + "</td><td>" + data.Timesheet.Comment + "</td></tr>");
                document.getElementById("ClockedTime").innerHTML = pretty_time_string(data.ClockedInTimeSpan.Hours) + " : " + pretty_time_string(data.ClockedInTimeSpan.Minutes) + " : " + pretty_time_string(data.ClockedInTimeSpan.Seconds);
            }
        });
        resetTimer();
        window.localStorage.removeItem(userId);
    })

    function dateToTime(date) {
        var d = new Date(parseInt(date.substr(6)));
        return d.toLocaleTimeString('en-US');
    }

    function pretty_time_string(num) {
        return (num < 10 ? "0" : "") + num;
    }

    function startTimer() {
        ++totalSeconds;
        hour = Math.floor(totalSeconds / 3600);
        minute = Math.floor((totalSeconds - hour * 3600) / 60);
        seconds = totalSeconds - (hour * 3600 + minute * 60);
        document.getElementById("timer").innerHTML = pretty_time_string(hour) + " : " + pretty_time_string(minute) + " : " + pretty_time_string(seconds);
        window.localStorage.setItem(userId, dateStarted);
    }
    function resetTimer() {
        hour = 0;
        minute = 0;
        seconds = 0;
        totalSeconds = 0;
        document.getElementById("timer").innerHTML = "00 : 00 : 00";
    }
});