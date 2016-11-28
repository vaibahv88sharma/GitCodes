(function () {

	angular.module('app')
		.value('badgeService',{
			retreiveBadge : retreiveBadge
		});

	function retreiveBadge(minutesRead){		
		var badge = null;
		
		switch(true){
			case(minutesRead > 5000):
				badge = 'Book Worm';
				break;
			case(minutesRead > 2500):
				badge = 'Page Turner';
				break;
			default:
				badge = 'Getting Started';
		}		
		return badge;		
	}

}());