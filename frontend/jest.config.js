module.exports = {
    preset: 'jest-preset-angular',
    testEnvironment: 'jsdom',
    setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
    transformIgnorePatterns: ['node_modules/(?!@ngrx|ngx-socket-io)'],
    testMatch: ['**/?(*.)+(spec).ts']
  };