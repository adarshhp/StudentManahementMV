$(document).ready(function () {
    let qualificationIndex = $("#qualificationTable tbody tr").length;

    $("#addQualification").on("click", function () {
        const row = createQualificationRow(qualificationIndex);
        $("#qualificationTable tbody").append(row);
        qualificationIndex++;
        reparseValidation();
    });

    $("#qualificationTable").on("click", ".remove-qualification", function () {
        $(this).closest("tr").remove();
        reindexQualifications();
    });

    $(".profile-photo-input").on("change", function () {
        const file = this.files && this.files[0];
        const preview = $("#profileImagePreview");

        if (!file || preview.length === 0) {
            return;
        }

        const reader = new FileReader();

        reader.onload = function (event) {
            preview.attr("src", event.target.result);
            preview.removeClass("d-none");
        };

        reader.readAsDataURL(file);
    });

    $("[data-confirm-delete]").on("click", function (event) {
        const message = $(this).data("confirm-delete");

        if (!confirm(message)) {
            event.preventDefault();
        }
    });

    function createQualificationRow(index) {
        return `
            <tr>
                <td>
                    <input class="form-control" type="text"
                           id="Qualifications_${index}__CourseName"
                           name="Qualifications[${index}].CourseName"
                           data-val="true"
                           data-val-required="Course name is required."
                           data-val-length="Course name cannot exceed 100 characters."
                           data-val-length-max="100" />
                    <span class="text-danger small field-validation-valid"
                          data-valmsg-for="Qualifications[${index}].CourseName"
                          data-valmsg-replace="true"></span>
                </td>
                <td>
                    <input class="form-control" type="text"
                           id="Qualifications_${index}__University"
                           name="Qualifications[${index}].University"
                           data-val="true"
                           data-val-required="University is required."
                           data-val-length="University cannot exceed 100 characters."
                           data-val-length-max="100" />
                    <span class="text-danger small field-validation-valid"
                          data-valmsg-for="Qualifications[${index}].University"
                          data-valmsg-replace="true"></span>
                </td>
                <td>
                    <input class="form-control" type="number"
                           id="Qualifications_${index}__PassingYear"
                           name="Qualifications[${index}].PassingYear"
                           min="1950"
                           max="2100"
                           data-val="true"
                           data-val-required="Passing year is required."
                           data-val-range="Passing year must be between 1950 and 2100."
                           data-val-range-min="1950"
                           data-val-range-max="2100" />
                    <span class="text-danger small field-validation-valid"
                          data-valmsg-for="Qualifications[${index}].PassingYear"
                          data-valmsg-replace="true"></span>
                </td>
                <td>
                    <input class="form-control" type="number"
                           id="Qualifications_${index}__Percentage"
                           name="Qualifications[${index}].Percentage"
                           min="0"
                           max="100"
                           step="0.01"
                           data-val="true"
                           data-val-required="Percentage is required."
                           data-val-range="Percentage must be between 0 and 100."
                           data-val-range-min="0"
                           data-val-range-max="100" />
                    <span class="text-danger small field-validation-valid"
                          data-valmsg-for="Qualifications[${index}].Percentage"
                          data-valmsg-replace="true"></span>
                </td>
                <td class="text-center">
                    <button type="button" class="btn btn-sm btn-outline-danger remove-qualification">
                        Remove
                    </button>
                </td>
            </tr>`;
    }

    function reindexQualifications() {
        $("#qualificationTable tbody tr").each(function (rowIndex) {
            $(this).find("input").each(function () {
                const currentName = $(this).attr("name");

                if (!currentName || currentName.indexOf("].") === -1) {
                    return;
                }

                const propertyName = currentName.substring(currentName.indexOf("].") + 2);
                const newName = `Qualifications[${rowIndex}].${propertyName}`;
                const newId = `Qualifications_${rowIndex}__${propertyName}`;

                $(this).attr("name", newName);
                $(this).attr("id", newId);
            });

            $(this).find("span[data-valmsg-for]").each(function () {
                const currentTarget = $(this).attr("data-valmsg-for");

                if (!currentTarget || currentTarget.indexOf("].") === -1) {
                    return;
                }

                const propertyName = currentTarget.substring(currentTarget.indexOf("].") + 2);
                $(this).attr("data-valmsg-for", `Qualifications[${rowIndex}].${propertyName}`);
            });
        });

        qualificationIndex = $("#qualificationTable tbody tr").length;
        reparseValidation();
    }

    function reparseValidation() {
        const form = $("#studentForm");

        if (form.length === 0 || !$.validator || !$.validator.unobtrusive) {
            return;
        }

        form.removeData("validator");
        form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
    }
});
