$(document).ready(function() {
	user = readCookie('session-id');
	if (user != null) {
			
			$('.loged-in-user').show();
			$('.not-loged-in-user').remove();
			   var fs = Tempo.prepare('marx-brothers');
				fs.starting();
	
		$.getJSON("ufs.json", function(data) {
		    fs.render(data);

			$('.butt2').mousedown(function() {
				$(this).addClass("hilight2");
			}).mouseup(function() {
				$(this).removeClass("hilight2");
			}).mouseover(function() {
				$(this).addClass("border2");
			}).mouseout(function() {
				$(this).removeClass("border2");
				$(this).removeClass("hilight2");
			});
		});
	}
	
		
	$('.lst .uf .butt2').live('mouseup', function(eventObj) {
	   // alert(this.id);
		
		th =  $(this);
		var elemZIndex = $(this).css('z-index', '100');
		var elemPos = $(this).offset();
		
		var ran_unrounded=Math.random()*50000;
		var ran_number = Math.floor(ran_unrounded);
		var newer = "newer" +  ran_number;
		var newer_id = "#" + newer;
		
		$(this).append('<div id=\"'+newer+'\" class="new" style="position:relative; '+ ' top:' + -15 + 'px; z-index:' + (elemZIndex + 10) + '">&nbsp;</div>');

		$(newer_id).grumble({
			text: "  <a href='/" + this.id + "' target='_blank' class='logout-but'><h3>Download</h3></a> <br/> <a href='#' ><h3>Cancel</h3></a>    ",
			angle: (Math.random() * 50 + 190),
			distance: 3,
			showAfter: 15,
			hideAfter: false,
			//type : 'alt-',
			hasHideButton: false,
			hideOnClick : true,
			// just shows the button
			onShow: function() {
				th.addClass("border2");
			},
			onBeginHide: function() {
				$('.border2').removeClass("border2");
			},
			onHide: function() {
				$(".new").empty();
				$(".new").detach();
				$(".new").remove();
			}
		});

	}).live('mousedown', function(){
		$(this).addClass("hilight2");
	}).live('mouseup', function(){
		$(this).removeClass("hilight2");
	}).live('mouseover', function(){
		$(this).addClass("border2");
	}).live('mouseout', function(eventObj){
		$(this).removeClass("border2");
		$(this).removeClass("hilight2");
	});
		
});