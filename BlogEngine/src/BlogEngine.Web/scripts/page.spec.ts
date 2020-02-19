

import { hello, App } from './page';

import { expect } from 'chai';

import 'mocha';



describe('Hello function', () => {
    it('should return hello world', () => {
        const result = hello();
        expect(result).to.equal('Hello world!');
    });
});



describe('GetAllAuthors', () => {
    it('return all authors', async () => {
        const display = new App();
        const actual = await display.getAllAuthors();
        expect(actual.length).to.equal(0);
    });
});