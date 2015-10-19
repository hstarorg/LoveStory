$(function () {
  // 屏幕玫瑰
  $(document).snowfall({
    image: 'assets/images/rose.gif',
    minSize: 10,
    maxSize: 32,
    shadow: true,
    round: true
  });
  var bgRandomArr = [
    'assets/images/bg/bg1.jpg',
    'assets/images/bg/bg2.jpg',
    'assets/images/bg/bg3.jpg',
    'assets/images/bg/bg4.jpg',
    'assets/images/bg/bg5.jpg',
    'assets/images/bg/bg6.jpg'
  ].sort(function (x1, x2) {
    return Math.random() < Math.random();
  });
  //背景图
  $('body').backstretch(bgRandomArr, {
    fade: 2000,
    duration: 5000
  });
  //左边区域心形图案
  $('#roseCanvas').drawHeart();
  //右边打字机
  var $text = $('#text'),
    left = $('#roseCanvas').offset().left + 220;
  $text.css('left', left + 'px');
  $text.typed({
    strings: ['亲爱的笨笨：<br /><br />对你有爱也有痛。<br />爱是一种甜蜜，痛是一种无奈。<br />对你的爱与痛加起来，那叫—爱情！<br />有爱就有痛——我心甘情愿。<br /><br />七夕情人节快乐！'],
    typeSpeed: 10,
    contentType: 'html'
  });
  //播放音乐
  document.getElementById('notice').play();
});