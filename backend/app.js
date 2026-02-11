let express = require('express');
let path = require('path');
let logger = require('morgan');
let Ajv = require('ajv');

var userschema = require('./schemas/userschema.json');
var validateSchema = require('./middleware/validate');
var isAuthenticated = require('./middleware/auth');

let indexRouter = require('./routes/index');
let vocabularyRouter = require('./routes/vocabularyRouter');

let app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(express.static(path.join(__dirname, 'build')));

app.use('/', indexRouter);
app.use('/vocabulary', vocabularyRouter);

module.exports = app;