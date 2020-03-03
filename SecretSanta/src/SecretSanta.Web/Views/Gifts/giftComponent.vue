<template>
    <div>
        <button class="button" @click="createGift()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Url</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="gift in gifts" :id="gift.id">
                    <td>{{gift.id}}</td>
                    <td>{{gift.title}}</td>
                    <td>{{gift.description}}</td>
                    <td>{{gift.url}}</td>
                    <td>
                        <button class="button" @click='setGift(gift)'>Edit</button>
                        <button class="button" @click='deleteGift(gift)'>Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <gift-details-component v-if="selectedGift != null"
                                  :gift="selectedGift"
                                  @gift-saved="refreshGifts()"></gift-details-component>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator'
    import { Gift, GiftClient } from '../../blogengine-client.ts';
    import GiftDetailsComponent from './giftDetailsComponent.vue';
    @Component({
        components: {
            GiftDetailsComponent
        }
    })
    export default class GiftsComponent extends Vue {
        gifts: Gift[] = null;
        selectedGift: Gift = null;
        async loadGifts() {
            let giftClient = new GiftClient();
            this.gifts = await giftClient.getAll();
        }
        createGift() {
            this.selectedGift = <Gift>{};
        }
        async mounted() {
                    await this.loadGifts();
        }
        setGift(gift: Gift) {
                    this.selectedGift = gift;
        }
        async refreshGifts() {
                    this.selectedGift = null;
            await this.loadGifts();
        }
        async deleteGift(gift: Gift) {
                    let giftClient = new GiftClient();
                if (confirm(`Are you sure you want to delete ${gift.firstName} ${gift.lastName}`)) {
                    await giftClient.delete(gift.id);
                }
                await this.refreshGifts();
        }
    }
</script>