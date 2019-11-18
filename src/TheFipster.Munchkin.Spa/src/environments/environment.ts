export const environment = {
  production: false,
  authServiceUri: 'http://localhost:5000/',
  gameServiceUri: 'http://localhost:5001/',
  gameApiUrl: 'http://localhost:5001/api/',
  identity: {
    authority: 'http://localhost:5000',
    client_id: 'game-spa',
    redirect_uri: 'http://localhost:4200/callback',
    response_type: 'code',
    scope: 'openid profile email game-api',
    post_logout_redirect_uri: 'http://localhost:4200'
  }
};
