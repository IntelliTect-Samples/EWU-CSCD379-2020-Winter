<template>
    <div>
        <h2>Gifts</h2>
        <button id="createGiftButton" class="button is-secondary" @click="create()">Create New</button>
        <table id="giftTableList" class="table">
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
                <tr v-for="gift in gifts" :key="gift.id">
                    <td>{{gift.id}}</td>
                    <td>{{gift.title}}</td>
                    <td>{{gift.description}}</td>
                    <td><a :href="gift.url" target="_blank">{{gift.url}}</a></td>
                    <td>
                        <button class="button is-primary" @click="edit(gift)">Edit</button>
                        <button class="button" @click="deleteGift(gift)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <gift-details-component v-if="selectedGift != null"
                                 :gift="selectedGift"
                                 @gift-saved="refresh()"></gift-details-component>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Gift, GiftClient } from '../../secretsanta-client';
    import GiftDetailsComponent from './giftDetailsComponent.vue';

    @Component({
        components: {
            GiftDetailsComponent
        }
    })
    export default class GiftsComponent extends Vue {
        gifts: Gift[] = null;
        selectedGift: Gift = null;
        giftClient: GiftClient;

        constructor() {
            super();
            this.giftClient = new GiftClient();
        }

        async mounted() {
            this.gifts = await this.giftClient.getAll();
        }

        create() {
            this.selectedGift = <Gift>{};
        }

        edit(gift: Gift) {
            this.selectedGift = gift;
        }

        async deleteGift(gift: Gift) {
            if (confirm(`Are you sure you want to delete ${gift.title}?`)) {
                await this.giftClient.delete(gift.id);
                this.refresh();
            }
        }

        async refresh() {
            this.gifts = await this.giftClient.getAll();
            this.selectedGift = null;
        }
    }
</script>