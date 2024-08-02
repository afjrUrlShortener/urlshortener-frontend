/** @type {import("tailwindcss").Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  theme: {
    extend: {
      colors: {
        primary: '#384078',
        secondary: '#A6B1E1',
        tertiary: '#DCD6F7',
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
