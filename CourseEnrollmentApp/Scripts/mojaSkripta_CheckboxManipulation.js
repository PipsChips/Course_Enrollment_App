$(document).ready(function () {
    var $questionMark = $(this).find("i.fa");
    $questionMark.click(function () {
        var infoMssg = "If enrollment's status is <i>Undefined</i>, you can only <i>Accept*</i>/<i>Decline</i> it.<br />"
            + "If enrollment's status is <i>Accepted</i>, you can only <i>Decline</i>/<i>Archive</i> it.<br />"
            + "If enrollment's status is <i>Declined</i>, you can only <i>Accept*</i>/<i>Archive</i> it.<br />"
            + "If enrollment's status is <i>Archived</i>, you can only <i>Delete</i> it."
            + "<br /><br />* You can only accept enrollment(s) if associated course isn't already filled.";
        $('.modal p').html(infoMssg);
        $('.modal').modal('show');
    });

    if ($("input[type='checkbox']:checked").length === 0) {
        $(this).find("button[name='action']").attr('disabled', true);
    }

    var counter = 0;
    $("input[type='checkbox']").click(function () {
        if (counter === 0) {            
            $(this).closest("form").find("button[name='action']").attr('disabled', false);
            counter++;
        }

        var $parentRow = $(this).closest('tr');
        var $parentTable = $(this).closest("table");
        var courseId = $parentRow.find("input[name = 'item.Course.CourseId']").val();
        var courseName = $parentRow.find("td").eq(7)[0].innerText;
        var enrollmentStatus = $parentRow.find("td").eq(10)[0].innerText;

        var $placesLeftOnThisCourse = $parentTable.find("input[name='item.Course.CourseId'][value='" + courseId + "']").closest("tr").find("td[id='placesLeft']");
        var placesLeftOnThisCourse = parseInt($placesLeftOnThisCourse.html());

        var $checkedDeclinedAndUndefinedRows = $parentTable.find("tr:contains('Declined'), tr:contains('Undefined')").find("[type=checkbox]:checked").closest("tr");
        var $courseIdElmntsOfcheckedDeclinedAndUndefinedRows = $checkedDeclinedAndUndefinedRows.find("#courseId").children();
        var list = $courseIdElmntsOfcheckedDeclinedAndUndefinedRows.map(function () { return $(this).attr("value"); }).get();
        var listOfDistinctCourseIdsOfCheckedDecleinedAndUndefinedRows = list.filter(onlyUnique);

        var $acceptSelectedButton = $(this).closest("form").find("button[name='action'][value='accept']");
        var $declineSelectedButton = $(this).closest("form").find("button[name='action'][value='decline']");
        var $archiveSelectedButton = $(this).closest("form").find("button[name='action'][value='archive']");
        var $deleteSelectedButton = $(this).closest("form").find("button[name='action'][value='delete']");

        var $checkedAcceptedRows = $parentTable.find("tr:contains('Accepted')").find("[type=checkbox]:checked");
        var $checkedDeclinedRows = $parentTable.find("tr:contains('Declined')").find("[type=checkbox]:checked");
        var $checkedArchivedRows = $parentTable.find("tr:contains('Archived')").find("[type=checkbox]:checked");
        var $checkedUndefinedRows = $parentTable.find("tr:contains('Undefined')").find("[type=checkbox]:checked");

        var noticeMssg = "<p style='font-size:20px;'>Course <strong>'" + courseName + "'</strong> is already full! "
            + "<i>Decline</i>/<i>Archive</i> some of its already accepted enrollments or <i>Uncheck</i> the ones previously selected which are ready/waiting for acceptance. "
            + "Otherwise, you won't be able to use multiple selection for accepting students to the other courses, even if those courses are available at the moment. "
            + "Though, you'll still be able to <i>'Archive'</i> and then <i>'Delete'</i> selected enrollments.";

        var editThis_DropDownBtnGroup = $parentTable.find("div.btn-group button");
        var $allCheckedCheckBoxes = $parentTable.find("[type=checkbox]:checked");

        if (this.checked) {
            if (enrollmentStatus === "Accepted") {
                $acceptSelectedButton.attr('disabled', true);
                $deleteSelectedButton.attr('disabled', true);
            }
            else if (enrollmentStatus === "Declined") {
                $declineSelectedButton.attr('disabled', true);
                $deleteSelectedButton.attr('disabled', true);

                $placesLeftOnThisCourse.html(--placesLeftOnThisCourse);

                if (placesLeftOnThisCourse < 0) {
                    $acceptSelectedButton.attr('disabled', true);
                    $('.modal p').html(noticeMssg);
                    $('.modal').modal('show');
                }
            }
            else if (enrollmentStatus === "Archived") {
                $archiveSelectedButton.attr('disabled', true);
                $acceptSelectedButton.attr('disabled', true);
                $declineSelectedButton.attr('disabled', true);
            }
            else if (enrollmentStatus === "Undefined") {
                $deleteSelectedButton.attr('disabled', true);
                $archiveSelectedButton.attr('disabled', true);

                $placesLeftOnThisCourse.html(--placesLeftOnThisCourse);
                
                if (placesLeftOnThisCourse < 0) {
                    $acceptSelectedButton.attr('disabled', true);
                    $('.modal p').html(noticeMssg);
                    $('.modal').modal('show');
                }
            }

            $("#enrollmentsTable td[id='placesLeft']").each(setPlacesLeftCellToRedIfLessThanZero);
            editThis_DropDownBtnGroup.attr('disabled', true);
        }
        else {
            if (enrollmentStatus === "Declined" || enrollmentStatus === "Undefined") {
                $placesLeftOnThisCourse.html(++placesLeftOnThisCourse);
            }

            if ($checkedAcceptedRows.length === 0 && $checkedArchivedRows.length === 0
                && isThere_more_Checked_Declined_Or_Undefined_Enrollments_Per_Course_Than_Places_Left_In_Course() === false) {

                $acceptSelectedButton.attr('disabled', false);
            }

            if ($checkedArchivedRows.length === 0 && $checkedDeclinedRows.length === 0) {
                $declineSelectedButton.attr('disabled', false);
            }

            if ($checkedAcceptedRows.length === 0 && $checkedDeclinedRows.length === 0 && $checkedUndefinedRows.length === 0) {
                $deleteSelectedButton.attr('disabled', false);
            }

            if ($checkedArchivedRows.length === 0 && $checkedUndefinedRows.length === 0) {
                $archiveSelectedButton.attr('disabled', false);
            }

            $("#enrollmentsTable td[id='placesLeft']").each(setPlacesLeftCellToRedIfLessThanZero);

            if ($allCheckedCheckBoxes.length === 0) {
                editThis_DropDownBtnGroup.attr('disabled', false);
                $(this).closest("form").find("button[name='action']").attr('disabled', true);
                counter = 0;
            }
        }

        function onlyUnique(value, index, self) {
            return self.indexOf(value) === index;
        }

        function isThere_more_Checked_Declined_Or_Undefined_Enrollments_Per_Course_Than_Places_Left_In_Course()
        {
            if (listOfDistinctCourseIdsOfCheckedDecleinedAndUndefinedRows.length > 0) {
                var isThere;
                for (var i = 0; i < listOfDistinctCourseIdsOfCheckedDecleinedAndUndefinedRows.length; i++) {
                    var numOfPlacesLeftForThisCourse = parseInt($parentTable.find("input[name='item.Course.CourseId'][value='" +
                        listOfDistinctCourseIdsOfCheckedDecleinedAndUndefinedRows[i] + "']").closest("tr").find("td[id='placesLeft']").html());

                    if (numOfPlacesLeftForThisCourse >= 0) {
                        isThere = false;
                    }
                    else {
                        isThere = true;
                    }
                }
                console.log(isThere);
                return isThere;
            }
            else {
                return false;
            }
        }

        function setPlacesLeftCellToRedIfLessThanZero() {
            var thisCell = $(this);
            var cellValue = parseInt(thisCell.text());

            if (cellValue < 0) {
                thisCell.css("background-color", "#FF0000");
            }
            else {
                thisCell.css("background-color", "rgb(255, 255, 255)");
            }
        }
    });
});