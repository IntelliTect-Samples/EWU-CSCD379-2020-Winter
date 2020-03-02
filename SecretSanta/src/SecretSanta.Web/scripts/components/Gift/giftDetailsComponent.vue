<template>
    <div>
        <div class="field">
            <label class="label">Title</label>
            <div class="input" typeof="text" v-model="gift.title"></div>
        </div>
        <div class="field">
            <label class="label">Description</label>
            <div class="input" typeof="text" v-model="gift.description"></div>
        </div>
        <div class="field">
            <label class="label">Url</label>
            <div class="input" typeof="text" v-model="gift.url"></div>
        </div>
        <div class="field">
            <label class="label">UserId</label>
            <div class="control" typeof="text" v-model.number="gift.userId"></div>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button id="submit" class="button is-primary" @click.once="saveGift">Submit</button>
            </div>
            <div class="control">
                <a asp-action="Index" class="button is-light" @click="cancelEdit">Cancel</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Emit} from 'vue-property-decorator';
    import { Gift, GiftClient } from '../../secretsanta-client';
    @Component
    export default class GiftDetailsComponent extends Vue {
        @Prop()
        gift: Gift;

        constructor() {
            super();
        }

        mounted() {
            let tempGift = { ...this.gift };
            this.gift = <Gift>tempGift;
        }

        @Emit('gift-saved')
        async saveGift() {
            let giftClient = new GiftClient();
            if(this.gift.id > 0) {
                await giftClient.put(this.gift.id, this.gift);
            }
            else {
                await giftClient.post(this.gift);
            }
        }

        @Emit('gift-saved')
        cancelEdit() {

        }
    }
</script>


