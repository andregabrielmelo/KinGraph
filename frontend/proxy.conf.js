module.exports = {
  "/api": {
    target:
      process.env["services__web__api-http__0"],
    secure: process.env["NODE_ENV"] !== "development",
    logLevel: "debug",
    changeOrigin: true,
    pathRewrite: {
      "^/api": "" // Remove the /api prefix
    }
  },
};
