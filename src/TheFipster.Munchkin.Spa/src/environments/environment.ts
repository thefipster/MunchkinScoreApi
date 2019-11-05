export const environment = {
  production: false,
  authServiceUri: 'https://localhost:5001/',
  gameServiceUri: 'https://localhost:5003/',
  gameApiUrl: 'https://localhost:5003/api/',
  identity: {
    authority: 'https://localhost:5001',
    client_id: 'game-spa',
    redirect_uri: 'http://localhost:4200/callback',
    response_type: 'code',
    scope: 'openid profile email game-api',
    post_logout_redirect_uri: 'http://localhost:4200'
  }
};
