function WonFilter() {

    return function (item) {

        return item ? 'Already Won' : 'Not Yet Won';

    };
}

angular.module('umbraco.filters')
       .filter('wonfilter', WonFilter); 