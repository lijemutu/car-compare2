// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.body.addEventListener('htmx:afterSwap', function(evt) {
    if (evt.detail.target.id === 'car-list') {
        document.querySelectorAll('.add-to-compare').forEach(button => {
            button.addEventListener('click', function() {
                const carId = this.dataset.carId;
                htmx.ajax('POST', '/compare/addtocompare/' + carId, '#compare-table-container');
            });
        });
    }
});
