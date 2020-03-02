<template>
    <table class="table">
        <thead>
            <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Description</th>
                <th>URL</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            {
            <tr v-for"gift in gifts" :id="gift.Id">
                <td>{{gift.Id}}</td>
                <td>{{gift.Title}}</td>
                <td>{{gift.Description}}</td>
                <td>{{gift.Url}}</td>
                <td>
                </td>
            </tr>
            }
        </tbody>
    </table>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Gift, GiftClient } from '../../secretsanta-client';
    @Component
    export default class GiftsComponent extends Vue {
        gifts: Gift[] = null;
        async loadGifts() {
            let giftClient = new GiftClient();
            this.gifts = await giftClient.getAll();
        }

        async mounted() {
            await this.loadGifts();
        }
    }
</script>