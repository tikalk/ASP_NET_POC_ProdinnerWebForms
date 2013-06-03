function clearForm(f) {
    $(f+' :input').not(':button, :submit').val('');
    $(f+" .awf-lookup-field .awf-display").html('&nbsp;');
    $(f+" .awf-multilookup-field .awf-display").html('');
    $(f+" select.awf-display").change();
}
