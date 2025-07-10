import { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";

const OAuthCallback = () => {
  const [params] = useSearchParams();
  const navigate = useNavigate();
  const baseUrl = "https://localhost:7272";
  useEffect(() => {
    const code = params.get("code");
    if (code) {
      fetch(`${baseUrl}/api/auth/exchange`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ code }),
      })
        .then((res) => {
          if (res.ok) navigate("/contacts");
          else {
            console.error("OAuth exchange failed:", res);
            alert("OAuth exchange failed.");
          }
        })
        .catch((err) => {
          console.error("Fetch error:", err);
          alert("Network or server error during OAuth exchange.");
        });
    }
  }, []);

  return <div>Exchanging token...</div>;
};

export default OAuthCallback;
