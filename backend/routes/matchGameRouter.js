var express = require('express');
var router = express.Router();

const knex = require('../utils/db');

router.get('/lang/:language', async (req, res) => {
  const languageCode = req.params.language;
  try {
    const language = await knex('languages')
      .where('lang_code', languageCode)
      .first();

    if (!language) {
      return res.status(400).json({ error: 'Invalid language code' });
    }

    const words = await knex('words')
      .join('word_translations as t', 'words.id', 't.word_id')
      .where('t.language_id', language.id)
      .select(
        'words.id',
        't.text as word',
        't.audio',
        'words.image'
      );

    res.json(words);
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: 'Server fail' });
  }
})

module.exports = router;