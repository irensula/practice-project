const Ajv = require('ajv');
var ajv = new Ajv(); // options can be passed, e.g. {allErrors: true}

const validateSchema = (schema) => {
    return function(req, res, next){
        console.log("starting middleware2")
        const reqmethod = req.method;
        if(reqmethod === "POST" || reqmethod === "PUT"){
            const body = req.body;
            var validate = ajv.compile(schema);
            var valid = validate(body);
            if (!valid){
                console.log(validate.errors);
                return res.status(401).json(
                    { error: "check json-data" })
            } else {
                next();
            }
        }   
        else {
            next();
        }  
    }
}

module.exports = validateSchema;