import { Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import ContactForm from './components/ContactForm';
import OAuthCallback from './components/OAuthCallback';

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/oauth/callback" element={<OAuthCallback />} />
      <Route path="/contacts" element={<ContactForm />} />
    </Routes>
  );
}
