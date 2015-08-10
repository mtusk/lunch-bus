Polymer({
    is: 'lunch-bus-list',

    properties: {
        buses: Array,
        selectedBus: {
            type: Object,
            notify: true
        }
    },

    busesLoaded: function (e, detail) {
        var buses = detail.response;

        if (buses !== null) {
            this.selectedBus = buses[0];
        }
    },

    busTapped: function () {
        this.fire('bus-tapped');
    }
});
