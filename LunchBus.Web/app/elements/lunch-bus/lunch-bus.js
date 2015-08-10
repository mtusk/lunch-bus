Polymer({

    is: 'lunch-bus',

    _computeListWidth: function (isMobile) {
        // when in mobile screen size, make the list be 100% width to cover the whole screen
        return isMobile ? '100%' : '33%';
    },

    busTapped: function () {
        this.$.mainDrawerPanel.closeDrawer();
    }

});