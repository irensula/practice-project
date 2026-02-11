var express = require('express');
var router = express.Router();

const knex = require('../utils/db');

router.get('/', async (req, res) => {
    try {
    const words = await knex('vocabulary').select('*');
    res.json(words);
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: 'Ошибка сервера' });
  }
})

// router.get('/:id', (req, res) => {
//     const productId = req.params.id;
//     knex('product')
//         .where({ id: productId})
//         .first()
//         .then(product => {
//             if(product) {
//                 res.json(product);
//             } else {
//                 res.status(404).json({ error: "Product not found" });
//             }
//         })
//         .catch(err => {
//             console.error('Error fetching product: ', err.message);
//             res.status(500).json({ error: 'Failed to fetch product by ID' });
//         });
// })
module.exports = router;