import { render, screen } from '@testing-library/react';
import Student from './Student';

test('renders learn react link', () => {
  render(<Student />);
  const linkElement = screen.getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
});
