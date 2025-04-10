$(".students-not-found").hide()

$(".search-btn").on("click", async () => {
    let students = await fetch("https://localhost/api/Student", {
        method: "post",
        headers: { "Content-Type": "application/json" },
        body: `"${$(".group-field").val()}"`
    })
        .then(response => response.json())

    let studentsHtml = ""

    if (students.length != 0) {
        for (const i in students) {
            if (i % 2 == 0) {
                studentsHtml += `<tr class="table-secondary">`
            } else {
                studentsHtml += `<tr>`
            }

            studentsHtml += `<td>${students[i].lastname}</td>
                         <td>${students[i].firstname}</td>
                         <td>${students[i].specialization}</td>
                         <td>${students[i].institute}</td>
                         <td>${students[i].group}</td>
                         <td>${students[i].year}</td>
                         <td>${students[i].admissionYear}</td>
                         <td>${formatDate(new Date(students[i].birthdate))}</td>
                    </tr>`
        }

        $(".students").html(studentsHtml)
        $(".students-not-found").hide()
    } else {
        $(".students").empty()
        $(".students-not-found").show()
    }
})