<template>
    <div>
        <button class="button" @click="createGift()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>URL</th>
                    <th>User Id</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="gift in gifts" :id="gift.id" :key="gift.id">
                    <td>{{gift.id}}</td>
                    <td>{{gift.title}}</td>
                    <td>{{gift.description}}</td>
                    <td>{{gift.url}}</td>
                    <td>{{gift.userId}}</td>
                    <td>
                        <button class="button" @click='setGift(gift)'>Edit</button>
                        <button class="button" @click='deleteGift(gift)'>Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <gift-detail v-if="selectedGift != null"
                                  :gift="selectedGift"
                                  @gift-saved="refreshGifts()"></gift-detail>
    </div>
</template>

<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator'
    import { Gift, GiftClient } from '../../secretsanta-client';
    import GiftDetail from './GiftDetail.vue';
    @Component({
        components: {
            GiftDetail
        }
    })
    export default class Gifts extends Vue {
        gifts: Gift[] = null
        selectedGift: Gift = null;

        async loadGifts() {
            this.gifts = await new GiftClient().getAll();
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
                if (confirm(`Are you sure you want to delete ${gift.title}?`)) {
                    await new GiftClient().delete(gift.id);
                    await this.refreshGifts();
                }
        }
    }
</script>
