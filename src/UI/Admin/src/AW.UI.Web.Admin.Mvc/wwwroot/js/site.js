function getStatesProvinces(country, dropdown, url) {
    if (country === "") {
        dropdown.empty();
        dropdown.append('<option selected="true" disabled>--Select--</option>');
        dropdown.prop('selectedIndex', 0);
    }
    else {
        dropdown.empty();
        $.getJSON(url, function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.code).text(entry.name));
            });
        });
    }
}

function getSalesPersons(territory, dropdown, url) {
    if (territory === "") {
        dropdown.empty();
        dropdown.append('<option selected="true" disabled>--Select--</option>');
        dropdown.prop('selectedIndex', 0);
    }
    else {
        dropdown.empty();
        dropdown.append('<option selected="true" disabled>--Select--</option>');
        $.getJSON(url, function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.name.fullName).text(entry.name.fullName));
            });
        });
    }
}

function getFlag(countryRegionCode) {
    if (countryRegionCode == "AU") {
        return "../assets/vendor/flag-icon-css/flags/1x1/au.svg"
    }
    else if (countryRegionCode == "CA") {
        return "../assets/vendor/flag-icon-css/flags/1x1/ca.svg"
    }
    else if (countryRegionCode == "US") {
        return "../assets/vendor/flag-icon-css/flags/1x1/us.svg"
    }
}