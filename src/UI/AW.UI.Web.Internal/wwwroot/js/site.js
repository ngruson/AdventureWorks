function getStateProvinces(country, dropdown, url) {
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