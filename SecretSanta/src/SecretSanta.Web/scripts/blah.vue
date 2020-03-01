<template>
    <div>
        <div>Hello from blah</div>
        <input v-model="name" />
        <div>{{name}}</div>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    @Component
    export default class Blah extends Vue {
        name: string = null;

        constructor() {
            super();
        }
    }
</script>