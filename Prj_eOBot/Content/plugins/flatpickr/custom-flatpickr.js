// Flatpickr
//fecha reporte
var f1 = flatpickr(document.getElementById('basicFlatpickr'));
//fecha primer reporte
var f5 = flatpickr(document.getElementById('basicFlatpickr1'));
//fecha ultimo pago
var f6 = flatpickr(document.getElementById('basicFlatpickr2'));

var f2 = flatpickr(document.getElementById('dateTimeFlatpickr'), {
    enableTime: true,
    dateFormat: "Y-m-d H:i",
});
var f3 = flatpickr(document.getElementById('rangeCalendarFlatpickr'), {
    mode: "range",
});
var f4 = flatpickr(document.getElementById('timeFlatpickr'), {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    defaultDate: "13:45"
});