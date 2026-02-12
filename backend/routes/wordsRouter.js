var express = require('express');
var router = express.Router();

const knex = require('../utils/db');

router.get('/', async (req, res) => {
    try {
      const words = await knex('words').select('*');
      res.json(words);
    } catch (err) {
      console.error(err);
      res.status(500).json({ error: 'Server fail' });
    }
})

module.exports = router;