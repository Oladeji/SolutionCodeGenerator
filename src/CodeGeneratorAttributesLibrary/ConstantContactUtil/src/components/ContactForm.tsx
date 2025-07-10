import { useState } from "react";
const baseUrl = "https://localhost:7272";
const ContactForm = () => {
  const [email, setEmail] = useState("");
  const [first, setFirst] = useState("");
  const [last, setLast] = useState("");
  const [result, setResult] = useState<any>(null);

  const search = async () => {
    const res = await fetch(`${baseUrl}/api/contacts/search?email=${email}`);
    const data = await res.json();
    setResult(data);
  };

  const create = async () => {
    const res = await fetch(`${baseUrl}/api/contacts/create`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
      email,
      firstName: first,
      lastName: last,
      create_source: "Contact" // or "Contact" if required by your use case
    }),
    });
    const data = await res.json();
    setResult(data);
  };

  return (
    <div style={{ padding: "2rem" }}>
      <h2>Manage Contact</h2>
      <input placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
      <br />
      <input placeholder="First Name" value={first} onChange={(e) => setFirst(e.target.value)} />
      <br />
      <input placeholder="Last Name" value={last} onChange={(e) => setLast(e.target.value)} />
      <br />
      <button onClick={search}>Search</button>
      <button onClick={create}>Create</button>
      <pre>{result && JSON.stringify(result, null, 2)}</pre>
    </div>
  );
};

export default ContactForm;
