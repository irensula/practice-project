let express = require('express');
let path = require('path');
let logger = require('morgan');
let Ajv = require('ajv');

var userschema = require('./schemas/userschema.json');
var validateSchema = require('./middleware/validate');
var isAuthenticated = require('./middleware/auth');

let indexRouter = require('./routes/index');
let wordsRouter = require('./routes/wordsRouter');
let matchGameRouter = require('./routes/matchGameRouter');

let app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));

app.use('/words', wordsRouter);
app.use('/match-game', matchGameRouter);
app.use('/', indexRouter);

// app.use(express.static(path.join(__dirname, 'build')));
app.use('/cdn-assets', express.static('cdn-assets'));

module.exports = app;