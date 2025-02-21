const { defineConfig } = require("@vue/cli-service");
const path = require("path");

module.exports = defineConfig({
  transpileDependencies: true,
  outputDir: path.resolve(__dirname, "../../src/Calculator.Api/wwwroot"),
  publicPath: "./" 
});
