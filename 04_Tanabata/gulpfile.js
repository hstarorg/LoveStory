var gulp = require('gulp'),
  runSequence = require('run-sequence'),
  del = require('del'),
  concat = require('gulp-concat'),
  uglify = require('gulp-uglify');

gulp.task('default', function () {
  return runSequence(
    ['clean'], ['build'], ['watch']
  );
});

gulp.task('build', function (callback) {
  return runSequence(['clean'], ['assets.js', 'assets.css', 'staticFiles', 'js', 'css'], callback);
});

gulp.task('clean', function (callback) {
  return del('./dist/', callback);
});

gulp.task('assets.js', function () {
  return gulp.src([
      './src/assets/plugins/jquery/*.js',
      './src/assets/plugins/**/*.js',
      '!./src/assets/plugins/**/*.min.js'
    ]).pipe(concat('assets.js', {
      newLine: ';'
    }))
    .pipe(gulp.dest('./dist/assets/js/'));
});

gulp.task('assets.css', function () {
  return gulp.src([
      './src/assets/plugins/**/*.css',
    ]).pipe(concat('assets.css', {
      newLine: '\n'
    }))
    .pipe(gulp.dest('./dist/assets/css/'));
});

gulp.task('js', function () {
  return gulp.src([
      './src/assets/js/**/*.js'
    ]).pipe(concat('core.js', {
      newLine: ';'
    }))
    .pipe(gulp.dest('./dist/assets/js/'));
});

gulp.task('css', function () {
  return gulp.src([
    './src/assets/css/**/*.css'
  ]).pipe(concat('core.css', {
    newLine: '\n'
  })).pipe(gulp.dest('./dist/assets/css/'));
});

gulp.task('staticFiles', function () {
  return gulp.src([
      './src/index.html',
      './src/assets*/images*/**/*.*',
      './src/assets*/audio*/*.*'
    ])
    .pipe(gulp.dest('./dist/'));
});

gulp.task('watch', function () {
  return gulp.watch('./src/**/*.*', ['build']);
});