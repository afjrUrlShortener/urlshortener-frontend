/** @type {import("tailwindcss").Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  theme: {
    extend: {
      colors: {
        primary: '#DCD6F7',
        secondary: '#384078',
        tertiary: '#A6B1E1',
        success: '#387855',
        error: '#b92a2a',
      },
      width: {
        18: '4.5rem' /* 72px */,
      },
      height: {
        18: '4.5rem' /* 72px */,
      },
    },
  },
  plugins: [],
};
