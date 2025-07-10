const clientId = "a861af35-d728-4e4b-8151-5b1f048db025";
const redirectUri = "http://localhost:5176/oauth/callback";

const Login = () => {
  const login = () => {
    const state = Math.random().toString(36).substring(2); // simple random string
    const url = `https://authz.constantcontact.com/oauth2/default/v1/authorize?client_id=${clientId}&redirect_uri=${encodeURIComponent(
      redirectUri
    )}&response_type=code&scope=contact_data&state=${state}`;
    window.location.href = url;
  };

  return (
    <div style={{ padding: "2rem" }}>
      <h2>Login to Constant Contact</h2>
      <button onClick={login}>Login</button>
    </div>
  );
};

export default Login;
